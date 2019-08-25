using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Clarifai.API;
using Clarifai.DTOs.Inputs;
using System.IO;
using Clarifai.DTOs.Predictions;

namespace DietDiaryBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            // With `CLARIFAI_API_KEY` defined as an environment variable
            //var client = new ClarifaiClient();

            // When passed in as a string
            var client = new ClarifaiClient("ca331bf809ea4bb6aaa8bfcd091159c0");

            // When using async/await
            //var res = await client.PublicModels.GeneralModel
            //    .Predict(new ClarifaiURLImage("https://samples.clarifai.com/metro-north.jpg"))
            //    .ExecuteAsync()

            // When synchronous
            var res = client.PublicModels.GeneralModel
                .Predict(new ClarifaiURLImage("https://www.kingarthurflour.com/sites/default/files/recipe_legacy/1496-3-large.jpg"))
                .ExecuteAsync()
                .Result;

            var x = client.PublicModels.GeneralModel
                .Predict(new ClarifaiURLImage("C:\\Users\\owner\\Downloads\\download (5).jpg "))
                .ExecuteAsync()
                .Result;

            var y = client.PublicModels.GeneralModel.Predict(
                    new ClarifaiFileImage(File.ReadAllBytes("C:\\Users\\owner\\Downloads\\IMG_0526.JPG")))
                .ExecuteAsync().Result;

            var z = client.Predict<Concept>(
                    client.PublicModels.FoodModel.ModelID,
                    new ClarifaiURLImage("C:\\Users\\owner\\Downloads\\IMG_0526.JPG"))
                .ExecuteAsync().Result;

            var results = new List<string>();
            // Print the concepts
            foreach (var concept in res.Get().Data)
            {
                //results.Add(concept.Name + ": " + concept.Value);
                Console.WriteLine($"{concept.Name}: {concept.Value}");
                
            }
            Console.WriteLine();
            //foreach (var concept in z.Get().Data)
            //{
            //    //results.Add(concept.Name + ": " + concept.Value);
            //    Console.WriteLine($"{concept.Name}: {concept.Value}");

            //}

            var min = client.Predict<Concept>(
                    client.PublicModels.GeneralModel.ModelID,
                    new ClarifaiURLImage("https://www.kingarthurflour.com/sites/default/files/recipe_legacy/1496-3-large.jpg"),
                    minValue: 0.99M, maxConcepts: 5)
                .ExecuteAsync().Result;
            foreach (var concept in min.Get().Data)
            {
                //results.Add(concept.Name + ": " + concept.Value);
                Console.WriteLine($"{concept.Name}: {concept.Value}");

            }

            Console.ReadLine();
        }
    }

}