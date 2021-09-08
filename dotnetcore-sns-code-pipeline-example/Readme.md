# AWS Code Pipelines

This folder contains an AWS code pipeline to deploy a dotnet core API (with an endpoint to send text messages using SNS) as a lambda function.

The pipeline has following steps:

* Cheking out from GitHub repo
* Build
  * Enforce code formatting
  * Restore packages
  * Build
  * Run unit tests
  * Create lambda function package
  * Publish code coverage report
* Self update the code pipeline
* Create / Update lambda function

## Setting up

* Fork this repository.
* Open the `/codepipeline/pipeline-stack.yaml` file and update the text `Owner: Adipa-G` to `Owner: <your github organisation name, and ensure to use the same case>`
* Commit the file `/codepipeline/pipeline-stack.yaml`
* Open the AWS management console.
* Create a GitHub PAT (personal access token)
* Open the secrets manager and create a new secret named `github-pat-token` 
* Create a new secret value with key `token` within the secret `github-pat-token` and store the GitHub PAT created as the value.
* Create a new stack named `sns-api-pipeline-stack` (via Services -> Cloud Formation) using the `codepipeline/pipeline-stack.yaml` file.
* Open the code pipelines in the AWS management console and click on the `Release Change` button.
* This will execute the pipeline and create a new stack with both lambda functions
* A new stack named `sns-api-stack` will appear (check Services -> Cloud Formation)
* When you open the new stack and view the `Outputs` section, there is the URL for the new API 
* Test the API endpoint in the browser and it'll result in `Hello World`. Further you can test <URL for the new API>/api/health endpoint to see the controller method being executed.
* POST following payload to the <URL for the new API>/api/Messages to send it to the SQS Queue (Make sure the phone number is verified if the test is done in a sandbox account via [this](https://us-east-2.console.aws.amazon.com/sns/v3/home#/mobile/text-messaging) page)
```
{
	"phoneNumber": "+XXXXXXXXXXXX",
	"text": "test msg 1"
}
```

