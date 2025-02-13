# XAF Blazor Vector Search

This simple PoC project XAF blazor uses native vector support from SQL Azure [(for details)](https://learn.microsoft.com/en-us/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqldb-current)

Set the database connection in appsettings

```json
{
  "ConnectionStrings": {
	"DefaultConnection": "Server=.;Database=VectorSearch;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```	

In the AzureOpenAI section of the appsettings.json file, insert the access key for Microsoft's OpenAI service (Autocompletion and Embedded model).

Once the document is loaded, click on **Embedding document** to generate the vector of the document.

When chatting, a sorting operation will be performed based on the cosine similarity between the vectors of the document chunks and the vector of the entered phrase, and the top MaxRelevantChunks (in appsettings.json) will be taken to be used in the autocompletion context.
(model autocomplete and Embedding)


![Vector Search](Media/VectorSearch.gif)
