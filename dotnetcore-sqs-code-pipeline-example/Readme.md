# AWS Code Pipelines

This folder contains an AWS code pipeline to deploy,
* A dotnet core API (with an endpoint to send data to a SQS) as a lambda function
* A SQS Queue triggered lambda function

The pipeline has following steps:

* Cheking out from GitHub repo
* Build
  * Enforce code formatting
  * Restore packages
  * Build
  * Run unit tests
  * Create both lambda function packages
  * Publish code coverage report
* Self update the code pipeline
* Create / Update the both lambda functions

## Setting up

* Fork this repository.
* Open the AWS management console.
* Create a GitHub PAT (personal access token)
* Open the secrets manager and create a new secret named `github-pat-token` 
* Create a new secret value with key `token` within the secret `github-pat-token` and store the GitHub PAT created as the value.
* Create a new stack named `sqs-api-pipeline-stack` (via Services -> Cloud Formation) using the `codepipeline/pipeline-stack.yaml` file.
* Open the code pipelines in the AWS management console and click on the `Release Change` button.
* This will execute the pipeline and create a new stack with both lambda functions
* A new stack named `sqs-api-stack` will appear (check Services -> Cloud Formation)
* When you open the new stack and view the `Outputs` section, there is the URL for the new API 
* Test the API endpoint in the browser and it'll result in `Hello World`. Further you can test <URL for the new API>/api/health endpoint to see the controller method being executed.
* POST following payload to the <URL for the new API>/api/LapRecords to send it to the SQS Queue
```
{
	"track": "SPA",
	"driver": "Lewis Hamilton",
	"lapTime": "1:11.103"
}	
```
* The consumtion function would write the content of the messages into the logs. You can check it from the cloud watch logs for the the `SqsTriggerFunction`

