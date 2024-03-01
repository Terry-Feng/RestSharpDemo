Feature: user actions 
A simple example of specflow and restsharp

Scenario: Create user
	Given the user server is up and running
    When create a new user with name Tom and job QA
    Then the user created successfully

Scenario: Get user
    Given the user server is up and running
    When I get the user by id 8
    Then the user info returnd and name is Lindsay