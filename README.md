# LocalStack + .NET Demo

This repo demonstrates local AWS service emulation (SES, in this case) with LocalStack in a .NET environment.  
Read more about it [here](https://codemage.co/blog/4-emulating-aws-services-with-localstack-for-.net-developers).

# Prerequisites

- Visual Studio 2022 (recommended)
- .NET SDK
- Docker

# Getting started

1. Run `docker compose up` in the repo root.
2. Start the application via .NET CLI or Visual Studio.

# Demo
> [!WARNING]
> Assuming no configuration changes

**Visual Studio**

1. Open the `app.http`.
2. Execute the `POST https://localhost:7172/send-email` to invoke the `send-email` endpoint.
3. Execute the `GET http://localhost:4566/_aws/ses?email=sender@demo.app` to view the messages received by the LocalStack instance.

**Curl**

1. Run the following command to invoke the `send-email` endpoint:
    ```sh
    curl -X POST https://localhost:7172/send-email -H "Content-Type: application/json" -d '{
        "recipient": "hello@bob.com",
        "content": "Hello Bob!"
    }' --insecure
    ```

2. Run the following command to fetch the messages received by LocalStack:

    ```sh
    curl http://localhost:4566/_aws/ses
    ```