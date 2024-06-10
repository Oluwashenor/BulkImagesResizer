using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

internal class Program
{
    private static void Main(string[] args)
    {
        Chatting();
        List<string> validImageExtensions = [".bmp", ".jpeg", ".png"];
        var imagesPath = GetValidLocation();
        Console.WriteLine("Folder Found");
        string[] files = Directory.GetFiles(imagesPath);
        if (files.Length < 1)
        {
            Console.WriteLine("This folder is empty");
            return;
        }
        else
        {
            var validFiles = files.Where(x => validImageExtensions.Contains(Path.GetExtension(x).ToLower())).ToList();
            if (validFiles.Count < 1)
            {
                Console.WriteLine("This folder contains no image");
                return;
            }
            else
            {
                Console.WriteLine($"Your folder contains {validFiles.Count} Valid Images");
                int count = 0;
                var createFolder = Directory.CreateDirectory(Path.Combine(imagesPath, "compressed"));
                if (!createFolder.Exists)
                {
                    Console.WriteLine("Unable to Create your folder");
                    return;
                }
                foreach (var item in validFiles)
                {
                    count++;
                    Console.WriteLine($"Processing file {count}");
                    _ = ResizeImage(createFolder.FullName, item);
                    Console.WriteLine($"Completed file {count}");
                }
            }
        }
    }

    private static bool ResizeImage(string pathToSave, string imageFile)
    {
        try
        {
            using Image image = Image.Load(imageFile);
            image.Mutate(x => x.Resize(image.Width / 5, image.Height / 5));
            string fileToSave = Path.Combine(pathToSave, Path.GetFileName(imageFile));
            image.Save(fileToSave);
            image.Dispose();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message.ToString());
            return false;
        }
    }

    private static string GetValidLocation()
    {
        string imagesPath = string.Empty;
        var isValid = false;
        while (!isValid)
        {
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Console.WriteLine("");
            Console.WriteLine("***********************************");
            Console.WriteLine(@"Please enter the location of your images using this format Desktop\Folder\Folder or Documents\Something\Something or Pictures\Something\Something");
            string imagesLocation = Console.ReadLine();
            imagesPath = $@"{userDirectory}\{imagesLocation}"; ;
            Console.Write($"Is this the valid location of your images {imagesPath} ? ");
            var userInput = Console.ReadLine();
            if (new List<string> { "yes", "yea"}.Contains(userInput.ToLower()))
            {
                if (Path.Exists(imagesPath))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("You do not have this folder on your desktop");
                }
            }
        }
        return imagesPath;
    }

    private static void Chatting()
    {
        var isValid = false;
        List<string> validHowAreYouResponses = ["fine",
            "good",
            "ok",
            "alright",
            "great",
            "well",
            "not bad",
            "excellent",
            "decent",
            "wonderful",
            "fantastic",
            "superb",
            "okay",
            "satisfactory"];
        while (!isValid)
        {
            Console.WriteLine("Hi Baby!");
            Console.Write("How is my woman doing ? ");
            string userInput = Console.ReadLine();
            if (validHowAreYouResponses.Contains(userInput.ToLower()))
            {
                Console.WriteLine("Good, Oya you can start converting your images, its quite straight forward just follow the prompts");
                isValid = true;
            }
            else
            {
                Console.WriteLine("Lol, OverSabi, how do you manage to type a response i do not have in my response List, TRY AGAIN");
            }
        }
    }
}