using System.Text.Json;

// Клас Ingredient
public class Ingredient
{
    public int Id { get; set; }

    public string Name { get; set; } = "";

    public double Quantity { get; set; }

    public Ingredient() { }

    public Ingredient(int id, string name, double quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }
}

// Клас Recipe
public class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    public List<Ingredient> Ingredients { get; set; }

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
    }

    public Recipe(int id, string title, List<Ingredient> ingredients)
    {
        Id = id;
        Title = title;
        Ingredients = ingredients;
    }
}

// Репозиторій
public class RecipeRepository
{
    private List<Recipe> recipes = new List<Recipe>();

    // Додати рецепт
    public void Add(Recipe recipe)
    {
        recipes.Add(recipe);
    }

    // Отримати всі рецепти
    public List<Recipe> GetAll()
    {
        return recipes;
    }

    // Отримати рецепт по ID
    public Recipe? GetById(int id)
    {
        return recipes.FirstOrDefault(r => r.Id == id);
    }

    // Зберегти у JSON файл
    public async Task SaveToFileAsync(string filename)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        using FileStream fs = new FileStream(filename, FileMode.Create);

        await JsonSerializer.SerializeAsync(fs, recipes, options);
    }

    // Завантажити з JSON файлу
    public async Task LoadFromFileAsync(string filename)
    {
        if (File.Exists(filename))
        {
            using FileStream fs = new FileStream(filename, FileMode.Open);

            var loadedRecipes =
                await JsonSerializer.DeserializeAsync<List<Recipe>>(fs);

            if (loadedRecipes != null)
            {
                recipes = loadedRecipes;
            }
        }
    }
}

// Головна програма
class Program
{
    static async Task Main(string[] args)
    {
        RecipeRepository repository = new RecipeRepository();

        // Створення рецептів
        Recipe recipe1 = new Recipe(
            1,
            "Піцца",
            new List<Ingredient>
            {
                new Ingredient(1, "Тісто", 1),
                new Ingredient(2, "Сир", 200),
                new Ingredient(3, "Помідори", 3)
            }
        );

        Recipe recipe2 = new Recipe(
            2,
            "Салат",
            new List<Ingredient>
            {
                new Ingredient(4, "Огірок", 2),
                new Ingredient(5, "Помідор", 2),
                new Ingredient(6, "Олія", 50)
            }
        );

        // Додавання у репозиторій
        repository.Add(recipe1);
        repository.Add(recipe2);

        // Збереження у файл
        await repository.SaveToFileAsync("recipes.json");

        Console.WriteLine("Дані збережено у файл recipes.json");

        // Новий репозиторій
        RecipeRepository newRepository = new RecipeRepository();

        // Завантаження з файлу
        await newRepository.LoadFromFileAsync("recipes.json");

        Console.WriteLine("\nЗавантажені рецепти:");

        // Виведення результату
        foreach (var recipe in newRepository.GetAll())
        {
            Console.WriteLine($"\nРецепт: {recipe.Title}");

            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine(
                    $" - {ingredient.Name}, кількість: {ingredient.Quantity}"
                );
            }
        }
    }
}