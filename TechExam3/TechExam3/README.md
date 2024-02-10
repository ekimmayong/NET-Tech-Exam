# TechExam # 3

You are given an Employee API that can perform this basic functions
 - Create, Read, Update and Delete record
Your task is to create an unit testing for the whole application.
Your test should covered both Positive and Negative test and You may add your own custom validation.
You can choose whatever unit test library you want e.g MSTest, NUnit, and xUnit


# Example Test
Name must not be null or empty
Name must not contains this special characters "!@#$%^&*()_-=+;:<>?/\|"
Name must not exceed 100 characters

With this current validation, 

My unit test on Add and Edit should pass when I input a valid name that is not null and doesn't contains invalid inputs.

And it should fail if I input a null, special characters, and 200 character lenght Name.