using kyiv.Models;
using kyiv.Services;
using System.Text.Json;


namespace kyiv.Services
{
	public static class ShareDictionary
	{
        public static EventHandler Update;
        public static async Task ShareWordsFile(List<DictionaryWordsModel> words )
        {
            string js = JsonSerializer.Serialize<List<DictionaryWordsModel>>(words);
            string fn = "Dictionary.txt";
            string file = Path.Combine(FileSystem.CacheDirectory, fn);

            File.WriteAllText(file, js);

            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Share dictionary",
                File = new ShareFile(file)
            });
        }

        public static async Task ImportWordsFile(string tableRef)
        {
            string? line = "";
            try
            {
                var result = await FilePicker.Default.PickAsync(PickOptions.Default);
                
                if (result != null)
                {
                    if (result.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase) )
                    {
                        using (StreamReader reader = new StreamReader(result.FullPath))
                        {

                            line = await reader.ReadToEndAsync();
                        } 
                    }
                }

                if (line != null && line != "")
                {
                    List<DictionaryWordsModel> words = JsonSerializer.Deserialize<List<DictionaryWordsModel>>(line);
                    foreach (DictionaryWordsModel item in words)
                    {
                        DbProvider.AddDictionaryWords(item, tableRef);
                    }
                    Update?.Invoke(new object(), new EventArgs());
                }

            }
            catch (Exception ex)
            {
               
            }


        }

    }
}

