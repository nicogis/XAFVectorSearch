﻿using Microsoft.Extensions.Options;
using Microsoft.ML.Tokenizers;
using XAFVectorSearch.Blazor.Server.Settings;

namespace XAFVectorSearch.Blazor.Server.Services;

public class TokenizerService(IOptions<AzureOpenAISettings> settingsOptions)
{
    private readonly TiktokenTokenizer tokenizer = TiktokenTokenizer.CreateForModel(settingsOptions.Value.ChatCompletion.ModelId);

    public int CountTokens(string input)
        => tokenizer.CountTokens(input);
}
