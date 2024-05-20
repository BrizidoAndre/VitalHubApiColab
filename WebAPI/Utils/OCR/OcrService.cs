using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace WebAPI.Utils.OCR
{
    public class OcrService
    {
        private readonly string _subsctiptionKey = "ebcc6fc1c03c47de9a9e5bf54831dca4";
        private readonly string _endpoint = "https://cvvitalhubg01m.cognitiveservices.azure.com/";


        //método para reconhecer o caracteres (texto) a partir de uma imagem
        public async Task<string> RecognizeTextAsync(Stream imageStream)
        {
            try
            {
                //cria um client para API de Computer Vision
                var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subsctiptionKey))
                {
                    Endpoint = _endpoint
                };

                //faz a chamada para a API
                var ocrResult = await client.RecognizePrintedTextInStreamAsync(true, imageStream);

                //processa o resultado e retorna o texto reconhecido
                return ProcessRecognitionResult(ocrResult);
            }
            catch (Exception e)
            {
                return "Erro ao reconhecer o texto" + e.Message;
            }
        }


        private static string ProcessRecognitionResult(OcrResult result)
        {
            string recognizedText = "";

            //percorre todas as regiões
            foreach (var region in result.Regions)
            {
                //para cada região percorre cada linha
                foreach (var line in region.Lines)
                {
                    //para cada linha, percorre as palavras
                    foreach (var word in line.Words)
                    {
                        //adiciona cada palavra ao texto, separando com espaço
                        recognizedText += word.Text + " ";
                    }

                    //quebra de linha ao final da linha
                    recognizedText += "\n";
                }
            }


            //torna o texto
            return recognizedText;
        }
    }
}
