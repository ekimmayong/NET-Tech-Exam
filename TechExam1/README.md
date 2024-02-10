# TechExam1

You are given a post api that accept string as an input. 
Your task is to optimize the function that counts the number of unique characters in the given string.
Please explain why your solution is much more optimize than the current.
Put your explanations below.

# Input:

A string "input" consisting of lowercase and/or uppercase letters and may contain special characters and the string length is n.

# Output:

An integer representing the count of unique characters in the string.

# Example

The given string "pneumonoultramicroscopicsilicovolcanoconiosis" contains 3 unique characters: {'e', 't', 'v'}.

# TestInput

"input" :"Whispers of the Night In the quiet of the moon's embrace, Where shadows dance and secrets trace, The night unfolds its velvet wings, And stars ignite like ancient things. The breeze, a soft and tender kiss, Caresses leaves in whispered bliss, As crickets hum their lullabies, And dreams take flight in moonlit skies. Beneath the silver canopy, The world slows down, and hearts break free, For darkness holds a mystic spell, Where time stands still, and all is well. So close your eyes, my weary friend, And let the night its solace send, For in these hours, we find our grace,And dance with stars in silent space."


# Explanation:

### Services
 Modify code from this
 
     public async Task<int> GetNumberOfUniqueCharacterFromString(string input)
    {
    
        int count = input.ToCharArray().Where(x => input.ToCharArray().Where(y=> y == x).Count() == 1).ToList().Count();
    
        return count;
    }

to

    public async Task<UniqueCharacterResponse> GetNumberOfUniqueCharacterFromString(string input)
    {
        UniqueCharacterResponse response = new()
        {
            UniqueCharacterCount = input.Distinct().Count(c => input.Count(x => x == c) == 1)
        };
        return response;
    }
    
1. This method utilizes LINQ's `Distinct()` and `Count()` methods to determine the count of unique characters. It iterates over each character in the string and counts how many times each character appears. If a character appears only once, it is considered unique. Also instead of returning an int, I constructed the `UniqueCharacterResponse` class and add the value to the `UniqueCharacter` property and return the class.
2. This method uses `Distinct()` to get the unique number instead of nested `Where()` calls.
3. I consider this more efficient and more readable due to the use of discriptive method names (`Distinct()` and `Count()` ) making the code easier to understand.

### Controller
Modify code from this

    [HttpPost("countuniquecharacters")]
    public async Task<ActionResult> CountUniqueCharacters(UniqueCharacterRequest request)
    {
       
        UniqueCharacterResponse uniqueCharacterResponse = new UniqueCharacterResponse();
        uniqueCharacterResponse.UniqueCharacterCount = await _iStringService.GetNumberOfUniqueCharacterFromString(request.input);
        return Ok(uniqueCharacterResponse);
    }
    
to
    
    [HttpPost("countuniquecharacters")]
    public async Task<ActionResult> CountUniqueCharacters(UniqueCharacterRequest request)
    {
        var response = await _iStringService.GetNumberOfUniqueCharacterFromString(request.input);
    
        return Ok(response);
    }
    
1. Instead of constructing `new UniqueCharacterResponse` in the controller, this code relies fully in the service that encapsulate the response data in `UniquesCharacterResponse` object and return this object as part of HTTP response.