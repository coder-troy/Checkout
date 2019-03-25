# CheckoutLtd
Technical Test for Checkout Ltd.

## Design
This project is designed using Domain Driven Design and Hexagonal Architecture. This is a bounded context microservice for managing your basket. The project is split into 4 layers: Presentation -> Infrastructure -> Application -> Domain. The service does not have the concept of an order, but rather a basket, where products and potentially services (Postage, Discounts) can be added as an basket item.

### Presentation Layer
The Checkout.Basket.Server is the inputs to the services. Users will be able to manipulate their basket. all layers are brought together at this level. The Presentation Layer has knowledge of all layers

### Infrastructure Layer
The Checkout.Basket.Infrastructure project is where all the outputs implemented, like database connections, publishing messages on service buses and external api calls if needed. The Infrastructure layer know about the application and domain layer.

### Application Layer
The Checkout.Basket.Application project defines the application flow and orchestrating services. This is where application services are also defined such as timing services. The Application Layer only knows about the domain layer.

### Domain Layer
The Checkout.Basket.Domain, is where all the aggregate roots are defined, like the Basket and Product. The basket has all the business rules for how the basket should behave.

### Data
The Product is what I like to call a Shadow Aggregate, this means that it is replicated from another bounded context microservice asynchronously to the basket service. The object is no way complete, but for the simplicity of this test, I have not defined any fields, but something like unit cost would be valid. 

## Future Improvements
- Improve the naming of services and functions.
- Implement event sourcing into the basket, this would then be used to show how users interacts with their baskets.
- Convert this project to use AWS Lambdas, ApiGateway and DynamoDB.
- Implement the product shadowing with lambdas from the product bounded context.
- Implement logging.

## References
Domain Driven Design: https://www.amazon.co.uk/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215
Hexagonal Architecture: https://herbertograca.com/2017/11/16/explicit-architecture-01-ddd-hexagonal-onion-clean-cqrs-how-i-put-it-all-together/
