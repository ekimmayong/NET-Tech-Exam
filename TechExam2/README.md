# TechExam2

You are given an api that accept 2 integer as an input and returns an integer which is the rounded value of the sum of the 2 Interger input.
you will round up the sum based on this condition:
- If the difference between the sum and the next multiple of 5 is less than 3, round up to the next multiple of 5 else don't round it up.

Your task is to debug the project and make it work.

# Input:

2 int and should be both whole number.

# Output:

An integer representing the sum or the rounded up value.

# Example

The given FirstNumber = 13 and the SecondNumber = 10 the reponse should be 25 since the sum of 2 int is 23 the next multiple of 5 is 25

The given FirstNumber = 30 and the SecondNumber = 27 the reponse should be 57 since the sum of 2 int is 57 and the next multiple of 5 is 60 and 60 - 57 is 3;

# Solutions

### Controller
Modify code from this

    [HttpPost("RoundSumOfTwoNumber")]
    public async IActionResult RoundSumOfTwoNumber(CalculateRequest request)
    {
       CalculateResponse calculateResponse = new CalculateResponse();
       int result = await _iCalculatorService.RoundingSumOf2Int(request.FirstNumber, request.SecondNumber);
    
       return Ok(calculateResponse);
    }
    
to 

    [HttpPost("RoundSumOfTwoNumber")]
    public IActionResult RoundSumOfTwoNumber(CalculateRequest request)
    {
       var result = _iCalculatorService.RoundingSumOf2Int(request.FirstNumber, request.SecondNumber);
    
       return Ok(result);
    }

### Service
Modify code from this

    public async Task<int> RoundingSumOf2Int(int firstNumber, int secondNumber)
    {
        int roundedValue = 0;
        int sum = 0;
    
        int summ = firstNumber + secondNumber;
        roundedValue = (sum % 5) == 0 ? sum : sum + 5;
    
        return roundedValue;
    }
    
to this
    
    public CalculateResponse RoundingSumOf2Int(int firstNumber, int secondNumber)
    {
        int sum = firstNumber + secondNumber;
        int mod = (sum % 5);
    
        CalculateResponse response = new()
        {
            Answer = mod == 0 ? sum : sum + 5 - mod
        };
    
        return response;
    }

### Problems 
1. the variable summ is declared but never used in the further calculation
2. the variable sum was initialize to 0 and was used in the roundedValue variable and as never updated after the actual sum calculation.
3.  for this calculation `(sum % 5) == 0 ? sum : sum + 5`, this is not realy performing the round up
4. Since the operation is simple and short running task, we do not need to use async await.
5. Initialization of variables, `roundedValue` and sum is not neccessary in this operation since we are not updating it every time.
6. Receiving and Exception error in the startup code.
7. In the controller, it construct new `CalculateResponse` return it as a http response data which does not contain any calculated data.
8. initialize result that returns the `RoundingSumOf2Int` value and never been used.

### Solutions
1. Removed variable summ and used variable sum instead. also i added the formula in the varible instead of initializing it to `0`
2. Remove `roundedValue` variable and put the formula to the `Answer` property of the `CalculateRespose` object.
3. Declare new variable ` mod = (sum % 5)` to get the ramainder and modify formula from `(sum % 5) == 0 ? sum : sum + 5` to ` mod == 0 ? sum : sum + 5 - mod`. the `mod` return the remainder of any number divisible by `5`, in the formula, if the remainder in equal to `0` then it will take the `sum` value else the `sum` is added to `5` and subtract the ramainder to make it divisible by `5`.
4. Instead of returning an int value, i make use of the class `CalculateRespose` and return `Answer` with its calculated value before returning it to the controller.
5. Since the service is simple and Short-running task, I made it a synchronous operation by removing the `async/await`
6. To resolve the Exception error, we need to add `builder.Services.AddControlles()` service to  services related to controllers like processing HTTP requests, model binding, validations, content negotiation, etc. It also registers necessary dependencies for routing and handling HTTP requests.
7. To Address the problem in the controller, removed the constructed `CalculateResponse`  and let the controller rely on the service response.
8. Change `int result` to `var result` to cater the response from the service and use it as the response data.