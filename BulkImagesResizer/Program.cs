using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Chatting();
        var imagesPath = GetValidLocation();
        var testRun = PerformTestRun();
        int compressionLevel = GetCompressionLevel();
        Compression(imagesPath.Item1, imagesPath.Item2, compressionLevel, testRun);
    }

    static bool PerformTestRun()
    {
        Console.Write("Would you like a test run? (yes/no): ");
        string response = Console.ReadLine().Trim().ToLower();
        if (response != "yes" && response != "no")
        {
            Console.WriteLine("Invalid response. Please enter 'yes' or 'no'.");
            return false;
        }
        return response == "yes";
    }

    static int GetCompressionLevel()
    {
        int compressionLevel;
        while (true)
        {
            Console.Write("Please enter a compression level between 1 and 5: ");
            string input = Console.ReadLine().Trim();
            if (int.TryParse(input, out compressionLevel) && compressionLevel >= 1 && compressionLevel <= 5)
            {
                break;
            }
            Console.WriteLine("Invalid input. Compression level must be an integer between 1 and 5.");
        }
        return compressionLevel+1;
    }

    private static void Compression(string path, List<string> files, int compressionLevel = 1, bool testRun = false)
    {
        int count = 0;
        var createFolder = Directory.CreateDirectory(Path.Combine(path, "compressed"));
        if (!createFolder.Exists)
        {
            Console.WriteLine("Unable to Create your folder");
            return;
        }
        foreach (var item in files)
        {
            count++;
            var processed = ResizeImage(createFolder.FullName, item, compressionLevel);
            var message = processed ? "Successfully" : "Unsuccessfully";
            Console.WriteLine($"Completed file {count} {message}");
            if (testRun && count > 2) {
                Console.WriteLine("Test Run Completed");
                break;
            }
        }
        Console.WriteLine("Bye baby. I guess we are done.xoxoxo");
    }

    private static bool ResizeImage(string pathToSave, string imageFile, int compressionLevel)
    {
        try
        {
            using Image image = Image.Load(imageFile);
            image.Mutate(x => x.Resize(image.Width / compressionLevel, image.Height / compressionLevel));
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

    private static (bool, List<string>) ValidateFolderHasImages(string path)
    {
        string userInput = string.Empty;
        List<string> validImages = [];
        bool isValid = false;
        while (!isValid)
        {
            List<string> validImageExtensions = [".bmp", ".jpeg", ".png", ".jpg"];
            string[] files = Directory.GetFiles(path);
            if (files.Length < 1)
            {
                Console.WriteLine("This folder is empty");
                break;
            }
            else
            {
                var validFiles = files.Where(x => validImageExtensions.Contains(Path.GetExtension(x).ToLower())).ToList();
                if (validFiles.Count < 1)
                {
                    Console.WriteLine("This folder contains no image");
                    break;
                }
                else
                {
                    Console.WriteLine($"Your folder contains {validFiles.Count} Valid Images");
                    Console.WriteLine($"Is this Correct ? Should the process begin ?");
                    userInput = Console.ReadLine();
                    if (userInput.Contains("yes"))
                    {
                        validImages = validFiles;
                        isValid = true;
                        break;
                    }
                }
            }
        }
        return (isValid, validImages);
    }

    private static (string, List<string>) GetValidLocation()
    {
        List<string> validImages = [];
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
            if (new List<string> { "yes", "yea" }.Contains(userInput.ToLower()))
            {
                if (Path.Exists(imagesPath))
                {
                    var validateImagePresent = ValidateFolderHasImages(imagesPath);
                    if (validateImagePresent.Item1)
                    {
                        isValid = true;
                        validImages = validateImagePresent.Item2;
                    }
                }
                else
                {
                    Console.WriteLine("You do not have this folder on your desktop");
                }
            }
        }
        return (imagesPath, validImages);
    }

    private static void Chatting()
    {
        string userInput = string.Empty;
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
            Console.WriteLine("Hi");
            userInput = Console.ReadLine();
            Console.WriteLine("Whoooooooooooo pleaseeeeee");
            userInput = Console.ReadLine();
            if (userInput.ToLower().Contains("kele"))
            {
                Console.WriteLine("oh, Hi Baby!");
                Console.Write("How is my woman doing ? ");
                userInput = Console.ReadLine();
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
            else
            {
                Console.WriteLine("Lol, I do not know you oh, please try again");
            }
        }
    }
}