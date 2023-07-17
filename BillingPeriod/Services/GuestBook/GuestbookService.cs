using BillingPeriod.Models;
using Newtonsoft.Json;

namespace BillingPeriod.Services.GuestBook
{
    public class GuestbookService : IGuestbookService
    {

        private const string FilePath = "wwwroot/txt/ListaComentario.txt";
        private static int lastGuestbookId = 0;

        public async Task<List<Guestbook>> GetAll()
        {
            try
            {
                List<Guestbook> guestbooks = new List<Guestbook>();

                string[] lines = await File.ReadAllLinesAsync(FilePath);

                foreach (string line in lines)
                {
                    Guestbook guestbook = JsonConvert.DeserializeObject<Guestbook>(line);
                    guestbooks.Add(guestbook);
                }

                return guestbooks;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading the file. ", ex);
            }
        }


        public async Task<bool> AddGuestbook(Guestbook guestbook)
        {
            try
            {
                guestbook.Id = ++lastGuestbookId;
                string entryJson = JsonConvert.SerializeObject(guestbook);

                await using StreamWriter writer = new StreamWriter(FilePath, append: true);
                await writer.WriteLineAsync(entryJson);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error occurred while writing to the file.", ex);
            }
        }


        public async Task<bool> UpdateGuestbook(Guestbook guestbook)
        {
            try
            {
                List<Guestbook> guestbooks = new List<Guestbook>();

                string[] lines = await File.ReadAllLinesAsync(FilePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    Guestbook existingGuestbook = JsonConvert.DeserializeObject<Guestbook>(lines[i]);

                    if (existingGuestbook.Id == guestbook.Id)
                    {
                        // Actualizar los campos del Guestbook existente
                        existingGuestbook.Name = guestbook.Name;
                        existingGuestbook.Country = guestbook.Country;
                        existingGuestbook.Comment = guestbook.Comment;

                        // Serializar el Guestbook actualizado a JSON
                        string updatedEntryJson = JsonConvert.SerializeObject(existingGuestbook);

                        // Reemplazar la línea en el archivo
                        lines[i] = updatedEntryJson;
                        break;
                    }
                }

                // Escribir las líneas actualizadas en el archivo
                await File.WriteAllLinesAsync(FilePath, lines);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error trying to update the file. ", ex);
            }
        }


        public async Task<bool> Delete(int id)
        {
            try
            {
                string[] lines = await File.ReadAllLinesAsync(FilePath);

                List<string> updatedLines = new List<string>();


                foreach (string line in lines)
                {
                    Guestbook guestbook = JsonConvert.DeserializeObject<Guestbook>(line);
                    
                    // Agrega todas las lineas que sean diferentes a la del id proporcionado
                    // es decir, no agrega la linea del id proporcionado
                    if (guestbook.Id != id)
                    {
                        updatedLines.Add(line);
                    }

                }

                await File.WriteAllLinesAsync(FilePath, updatedLines);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error occurred while deleting the file. ", ex);
            }
        }

        public async Task<bool> ExistsGuestbook(int id)
        {
            try
            {
                string[] lines = await File.ReadAllLinesAsync(FilePath);

                foreach (string line in lines)
                {
                    Guestbook guestbook = JsonConvert.DeserializeObject<Guestbook>(line);
                    if (guestbook.Id == id)
                    {
                        return true; // El Guestbook con el ID proporcionado existe
                    }
                }

                return false; // El Guestbook con el ID proporcionado no existe
            }
            catch (Exception ex)
            {
                // Manejo personalizado de la excepción o relanzamiento según tus necesidades.
                // ...

                return false;
            }
        }

        public async Task<Guestbook> Get(int id)
        {
            try
            {
                List<Guestbook> guestbooks = new List<Guestbook>();

                string[] lines = await File.ReadAllLinesAsync(FilePath);

                foreach (string line in lines)
                {
                    Guestbook guestbook = JsonConvert.DeserializeObject<Guestbook>(line);
                    guestbooks.Add(guestbook);
                }

                Guestbook foundGuestbook = guestbooks.FirstOrDefault(g => g.Id == id);

                return foundGuestbook;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while reading the file and searching for the Guestbook. ", ex);
            }
        }
    }
}
