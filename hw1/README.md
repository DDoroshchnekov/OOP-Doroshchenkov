# Доповідь: Антипатерн God Object та SRP

## 1. Характеристики "God Object"

"God Object" — це антипатерн у програмуванні, коли один клас:

- Містить занадто багато відповідальностей;
- Має безліч методів і полів;
- Тісно залежить від багатьох інших класів;
- Ускладнює тестування, підтримку та розвиток коду;
- Порушує принципи SOLID, особливо SRP (Single Responsibility Principle).

---

## 2. Приклад класу, що порушує SRP

```csharp
public class UserManager
{
    public void SaveUser(User user) { /* Збереження користувача */ }
    public void SendEmail(User user, string message) { /* Відправка email */ }
    public void GenerateReport(User user) { /* Генерація звіту */ }
}
