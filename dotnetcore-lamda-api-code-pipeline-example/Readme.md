# AWS Code Pipelines

This folder contains an AWS code pipeline to deploy a dotnet core API into a lambda function.

## Setting up

* Fork this repository.
* Open the AWS management console.
* Create a GitHub PAT (personal access token) and store it in secrets manager as `github-pat-token`
* Open the secrets manager and create a new secret named `github-pat-token` 
* Create a new secret value with key `token` within the secret `github-pat-token` and store the GitHub PAT created as the value.
* Create a new stack named `lamda-api-pipeline-stack` (via Services -> Cloud Formation) using the `codepipeline/pipeline-stack.yaml` file.
* Open the code pipelines in the AWS management console and click on the `Release Change` button.
* This will execute the pipeline and create a new stack with the lambda function
* A new stak named `lamda-api-stack` will appear (check Services -> Cloud Formation)
* When you open the new stack and view the `Oputputs` section, there is the URL for the new API 
* Test the API endpoint in the browser and it'll result in `Hello World`. Further you can test <URL for the new API>/api/health endpoint to see the controller method being executed.

## References

https://github.com/scottjbaldwin/AWSCodePipelineExample

