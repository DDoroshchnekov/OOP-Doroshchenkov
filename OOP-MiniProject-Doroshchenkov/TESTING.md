# TESTING

## Run tests

dotnet test

## Run coverage

dotnet test /p:CollectCoverage=true

## Covered scenarios

- Borrow books
- Save books
- Load books
- Invalid title
- Invalid author
- Duplicate borrow
- Missing file
- Corrupted JSON