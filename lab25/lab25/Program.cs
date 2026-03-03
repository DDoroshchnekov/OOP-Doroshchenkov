using lab25;

static void PrintScenarioHeader(string title)
{
    Console.WriteLine();
    Console.WriteLine(new string('=', 50));
    Console.WriteLine(title);
    Console.WriteLine(new string('=', 50));
}

const string input = "Hello lab25!!!";

//
// Сценарій 1: Повна інтеграція
//
PrintScenarioHeader("Scenario 1: Full integration (Console logger + Encrypt strategy)");

LoggerManager.Instance.Initialize(new ConsoleLoggerFactory());

var context1 = new DataContext(new EncryptDataStrategy());
var publisher1 = new DataPublisher();

var observer1 = new ProcessingLoggerObserver();
observer1.Subscribe(publisher1);

LoggerManager.Instance.Log($"Main: Using strategy '{context1.CurrentStrategyName}'");

var processed1 = context1.ProcessData(input);
LoggerManager.Instance.Log($"Main: Processed data = {processed1}");

publisher1.PublishDataProcessed(processed1, context1.CurrentStrategyName);


//
// Сценарій 2: Динамічна зміна логера
//
PrintScenarioHeader("Scenario 2: Change logger dynamically (Console -> File)");

var logPath = Path.Combine(AppContext.BaseDirectory, "lab25.log");

// перша обробка (консоль)
var context2 = new DataContext(new EncryptDataStrategy());
var publisher2 = new DataPublisher();
var observer2 = new ProcessingLoggerObserver();
observer2.Subscribe(publisher2);

var first2 = context2.ProcessData(input);
publisher2.PublishDataProcessed(first2, context2.CurrentStrategyName);

// зміна фабрики логера на файлову
LoggerManager.Instance.SetFactory(new FileLoggerFactory(logPath));
LoggerManager.Instance.Log("Main: Logger switched to FileLoggerFactory");

// друга обробка (піде у файл)
var second2 = context2.ProcessData("Second run data");
publisher2.PublishDataProcessed(second2, context2.CurrentStrategyName);

Console.WriteLine($"(Check file) Log written to: {logPath}");


//
// Сценарій 3: Динамічна зміна стратегії
//
PrintScenarioHeader("Scenario 3: Change strategy dynamically (Encrypt -> Compress)");

// повернемо логер в консоль, щоб було видно
LoggerManager.Instance.SetFactory(new ConsoleLoggerFactory());

var context3 = new DataContext(new EncryptDataStrategy());
var publisher3 = new DataPublisher();
var observer3 = new ProcessingLoggerObserver();
observer3.Subscribe(publisher3);

// перша обробка Encrypt
var first3 = context3.ProcessData("AAAABBBCCDAA");
publisher3.PublishDataProcessed(first3, context3.CurrentStrategyName);

// зміна стратегії на Compress
context3.SetStrategy(new CompressDataStrategy());
LoggerManager.Instance.Log($"Main: Strategy switched to '{context3.CurrentStrategyName}'");

// друга обробка Compress
var second3 = context3.ProcessData("AAAABBBCCDAA");
publisher3.PublishDataProcessed(second3, context3.CurrentStrategyName);