# FootballManager
Technical assignment 
![.NET](https://github.com/bdeklerkdelta/FootballManager/workflows/.NET/badge.svg)

**Database Design**


![Database Design](https://i.ibb.co/hKC02N7/DbDesign.png)


**How to run the code**
Simply clone the repo and run the API project. Ensure that .NET 5.0 runtime or SDK is installed.
There is a Postman collection which contains every endpoint with sample data setup.


**Design decisions**

1. Mediator Pattern - I enjoy the Mediator pattern as it promotes readability, maintability as well as allows for a loose coupling between objects.

2. CQRS Pattern - Splitting out the read and write operations allows for clearing separation of conerns and thus increases the maintability, performance and scalability of the application.

3. Repository Pattern - This was done to allow easier abstraction of the data access. The repositories still leverage the DbContext that EF core provides but now we have addtional flexability when it comes to unit tests. We can also share a generic implementation of data access across the specific repository implemenetations.

4. SQL Lite in-memory database - This allowed me to have relational database model in-memory which is not the case with the regular EF core in-memory provider. I was able to use foregin key configuration in the entity classes to allow EF core to load related data as per usual.

5. Value Objects - These help with validation or domain logic linked to properties of an object. It also helps with ambiguity when using primitives instead. They are also useful to communicate concepts in the domain by simply reading the code.

**Obstacles encountered**

In the beginning I faced isues with EF core in-memory provider and maintaing relationships between the entities. As such, I swapped to the SQL Lite provider and this allowed for a relational database in-memory.

**Resources used**

1. Gang of Four - design patterns

2. Martin Fowler - value objects https://martinfowler.com/bliki/ValueObject.html

**Time taken to complete assignment**

10 hours

**What would I do differently or add to the API**
I would most likely use a physical datastore such as SQL server datatbase instance. The in-memory provider gave me a lot of headaches and issues.

I woud like to have added the following items to the API:

1. Authorization

2. More error handling - better error messsages returned as responses on the API

3. More use of value objects

4. More unit tests for the value objects

5. Logging to an external provider such that the performance and error rate of the application can be measured.

6. Adding integration tests as part of the Postman collection.
