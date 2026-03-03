Лекція №23
Юніт-тестування: теорія та практика

Юніт-тестування — це процес перевірки найменших одиниць програмного коду, зазвичай окремих методів або класів, ізольовано від інших компонентів системи. Юніт-тест є автоматизованим і дозволяє перевірити, чи працює конкретна частина програми відповідно до очікувань. Це одна з основних технік забезпечення якості програмного забезпечення.

Основна перевага юніт-тестування полягає в ранньому виявленні помилок. Розробник отримує зворотний зв’язок одразу після написання коду. Крім того, юніт-тести виконують роль документації, оскільки показують, як саме має працювати метод. Вони також дозволяють безпечно проводити рефакторинг, адже якщо зміни зламають логіку, тести одразу це покажуть. Код, який легко тестується, зазвичай має кращу архітектуру та слабке зв’язування між компонентами.

Однак юніт-тестування має і обмеження. Воно перевіряє лише окремі частини системи та не гарантує, що всі компоненти разом працюють правильно. Юніт-тести не перевіряють взаємодію з реальною базою даних, API або файловою системою. Тому для повної перевірки системи необхідні інтеграційні тести.

Юніт-тестування відрізняється від інтеграційного тим, що воно ізольоване та швидке. Інтеграційні тести перевіряють взаємодію кількох компонентів і можуть залежати від зовнішніх ресурсів. Юніт-тести виконуються швидше та дозволяють точніше визначити місце помилки. Інтеграційні тести забезпечують перевірку роботи системи в цілому.

Розглянемо приклад класу OrderService.

public class OrderService
{
    public decimal CalculateTotal(decimal price, int quantity)
    {
        if (price < 0 || quantity < 0)
            throw new ArgumentException("Invalid input");

        return price * quantity;
    }

    public decimal ApplyDiscount(decimal total, bool isVip)
    {
        if (total < 0)
            throw new ArgumentException("Total cannot be negative");

        return isVip ? total * 0.9m : total;
    }

    public bool IsLargeOrder(decimal total)
    {
        return total >= 1000;
    }
}

Метод CalculateTotal має тест для успішного сценарію.

[Fact]
public void CalculateTotal_ValidInput_ReturnsCorrectTotal()
{
    var service = new OrderService();
    var result = service.CalculateTotal(100, 2);
    Assert.Equal(200, result);
}

Також має тест для граничного випадку.

[Fact]
public void CalculateTotal_NegativeInput_ThrowsException()
{
    var service = new OrderService();
    Assert.Throws<ArgumentException>(() =>
        service.CalculateTotal(-10, 5));
}

Метод ApplyDiscount перевіряється успішним сценарієм.

[Fact]
public void ApplyDiscount_VipCustomer_ReturnsDiscountedTotal()
{
    var service = new OrderService();
    var result = service.ApplyDiscount(100, true);
    Assert.Equal(90, result);
}

І тестом граничного випадку.

[Fact]
public void ApplyDiscount_NegativeTotal_ThrowsException()
{
    var service = new OrderService();
    Assert.Throws<ArgumentException>(() =>
        service.ApplyDiscount(-50, true));
}

Метод IsLargeOrder також має два тести.

[Fact]
public void IsLargeOrder_TotalAbove1000_ReturnsTrue()
{
    var service = new OrderService();
    var result = service.IsLargeOrder(1500);
    Assert.True(result);
}
[Fact]
public void IsLargeOrder_TotalBelow1000_ReturnsFalse()
{
    var service = new OrderService();
    var result = service.IsLargeOrder(500);
    Assert.False(result);
}

Mock-об’єкти використовуються тоді, коли клас має залежності від інших компонентів, наприклад бази даних або зовнішнього сервісу. В такому випадку замість реального об’єкта створюється його імітація, яка дозволяє контролювати поведінку залежності. Це забезпечує ізоляцію тесту. Якщо клас не має зовнішніх залежностей, як у випадку OrderService, використання mock не є необхідним.

Таким чином, юніт-тестування є важливим інструментом забезпечення якості коду. Воно дозволяє перевіряти логіку системи ізольовано, швидко знаходити помилки та безпечно змінювати програму. Разом із інтеграційним тестуванням воно формує повний підхід до перевірки програмного забезпечення.