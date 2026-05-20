# IndependentWork21 Test Report

## Positive Scenarios

### 1. Factory creates correct strategy
Expected: Strategy object created successfully.
Actual: Success.

### 2. Singleton keeps same instance
Expected: Same object reference.
Actual: Success.

### 3. Observer receives notification
Expected: Observer outputs processed data.
Actual: Success.

## Negative Scenarios

### 1. Strategy is null
Expected: Error message returned.
Actual: "Strategy not selected".

### 2. Observer not subscribed
Expected: No crash during publish.
Actual: Success.

## Conclusion

All integration scenarios passed successfully.
Patterns Factory, Singleton, Strategy and Observer interact correctly.
System behavior is stable and extensible.