import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { createSummary, createAgenda, createInvite } from "../api/prompt";

export const generateAiContent = createAsyncThunk(
  "meeting/generateContent",
  async ({ titel, datum }, { getState, rejectWithValue }) => {
    const { activeTab, inputPrompt } = getState().meeting;

    if (!inputPrompt.trim()) {
      return rejectWithValue("Textfältet får inte vara tomt.");
    }

    const fullPrompt = `
    VIKTIGA REGLER:
    1. Om en titel är angiven nedan MÅSTE du använda EXAKT den titeln ordagrant. Hitta ALDRIG på en egen titel.
    2. Använd exakt det angivna datumet.

    Titel: ${titel || "Ingen angiven"}
    Datum: ${datum || "Inget angivet"}

    Anteckningar att sammanfatta:
    ${inputPrompt}
    `;

    try {
      let response;
      if (activeTab === "summarize") {
        response = await createSummary(fullPrompt);
      } else if (activeTab === "agenda") {
        response = await createAgenda(fullPrompt);
      } else if (activeTab === "invitation") {
        response = await createInvite(fullPrompt);
      }

      if (typeof response === "object" && response !== null) {
        return response.result || JSON.stringify(response, null, 2);
      }
      return response;
    } catch (error) {
      return rejectWithValue(error.message || "Något gick fel vid AI-anropet.");
    }
  },
);

const meetingSlice = createSlice({
  name: "meeting",
  initialState: {
    activeTab: "summarize",
    inputPrompt: "",
    result: "",
    loading: false,
    error: null,
  },
  reducers: {
    setActiveTab: (state, action) => {
      state.activeTab = action.payload;
      state.inputPrompt = "";
      state.result = "";
      state.error = null;
    },
    setInputPrompt: (state, action) => {
      state.inputPrompt = action.payload;
    },
    clearResult: (state) => {
      state.result = "";
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(generateAiContent.pending, (state) => {
        state.loading = true;
        state.error = null;
        state.result = "";
      })
      .addCase(generateAiContent.fulfilled, (state, action) => {
        state.loading = false;
        state.result = action.payload;
      })
      .addCase(generateAiContent.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload;
      });
  },
});

export const { setActiveTab, setInputPrompt, clearResult } =
  meetingSlice.actions;
export default meetingSlice.reducer;
