import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { createSummary, createAgenda, createInvite } from "../api/prompt";

export const generateAiContent = createAsyncThunk(
  "meeting/generateContent",
  async (_, { getState, rejectWithValue }) => {
    const { activeTab, inputPrompt } = getState().meeting;

    if (!inputPrompt.trim()) {
      return rejectWithValue("Textfältet får inte vara tomt.");
    }

    try {
      let response;
      if (activeTab === "summarize") {
        response = await createSummary(inputPrompt);
      } else if (activeTab === "agenda") {
        response = await createAgenda(inputPrompt);
      } else if (activeTab === "invitation") {
        response = await createInvite(inputPrompt);
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
