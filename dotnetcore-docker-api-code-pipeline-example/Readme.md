# AWS Code Pipelines

This folder contains an AWS code pipeline to deploy a dotnet core API into a docker image and deploy it into AWS Fargate.

The pipeline has following steps:

* Checking out from GitHub repo
* Build
  * Enforce code formatting
  * Restore packages
  * Build
  * Run unit tests
  * Create docker image
  * Publish docker image to the container registry
  * Publish code coverage report
* Self update the code pipeline
* Create / Update the ECS cluster running the API

## Setting up

* Fork this repository.
* Open the `/codepipeline/pipeline-stack.yaml` file and update the text `Owner: Adipa-G` to `Owner: <your github organisation name, and ensure to use the same case>`
* Commit the file `/codepipeline/pipeline-stack.yaml`
* Open the AWS management console.
* Create a GitHub PAT (personal access token)
* Open the secrets manager and create a new secret named `github-pat-token` 
* Create a new secret value with key `token` within the secret `github-pat-token` and store the GitHub PAT created as the value.
* Create a new stack named `fargate-api-pipeline-stack` (via Services -> Cloud Formation) using the `codepipeline/pipeline-stack.yaml` file.
* Open the code pipelines in the AWS management console and click on the `Release Change` button.
* This will execute the pipeline and create a new stack with a Fargate ECS cluster
* A new stack named `fargate-api-stack` will appear (check Services -> Cloud Formation)
* When you open the new stack and view the `Oputputs` section, there is the URL for the new API 
* Test the API endpoint in the browser and it'll result in `Hello World`. Further you can test <URL for the new API>/api/health endpoint to see the controller method being executed.

## Variations

There are multiple templates for the Fargate cluster, and you can select which template to be used by updating the file codepipeline/pipeline-stack.yaml

* cloudformationtemplates\template-public-subnet.yaml creates the ECS cluster in a public subnet
* cloudformationtemplates\template-private-subnet.yaml creates ECS cluster in a private subnet.



## References

https://github.com/1Strategy/fargate-cloudformation-example
https://github.com/nathanpeck/aws-cloudformation-fargate

