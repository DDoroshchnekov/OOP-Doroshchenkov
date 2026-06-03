# CI/CD Report

## Workflows

### CI

Виконує автоматичну перевірку проєкту після push та pull request.

Функції:

* restore залежностей;
* перевірка форматування;
* build;
* запуск тестів;
* збір coverage;
* збереження артефактів.

### Docker Build

Виконує збірку Docker-образу та перевірку запуску контейнера.

### Manual Release

Дозволяє вручну створювати реліз через вкладку Actions.

---

## Використані тригери

* push
* pull_request
* workflow_dispatch

---

## Додавання нового check

Новий check додається через створення нового step у workflow:

```yaml
- name: New Check
  run: echo "Check"
```

---

## Matrix Strategy

Matrix дозволяє запускати один workflow для різних комбінацій параметрів.

У проєкті використовуються:

* ubuntu-latest
* windows-latest
* .NET 8
* .NET 9

Таким чином створюються 4 незалежні запускі.

---

## Artifacts

Artifacts використовуються для збереження результатів роботи workflow.

У проєкті зберігаються:

* coverage.cobertura.xml
* release artifacts

Знайти їх можна:

Actions → конкретний запуск → Artifacts.

---

## Результати

Усі workflow успішно виконані.

Додано скріншоти:

* успішний CI pipeline;
* успішний Docker Build;
* успішний Manual Release;
* приклад невдалого pipeline.
