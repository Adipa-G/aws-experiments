# AWS Code Pipelines

This folder contains an AWS code pipeline to deploy two Grails applications, with load balancer configured to route traffic to each application.

The pipeline has following steps:

* Checking out from GitHub repo
* Build both Grails applications
* Self update the code pipeline
* Create / Update the ECS cluster running applications

## Setting up

* Fork this repository.
* Open the `/codepipeline/pipeline-stack.yaml` file and update the text `Owner: Adipa-G` to `Owner: <your github organisation name, and ensure to use the same case>`
* Commit the file `/codepipeline/pipeline-stack.yaml`
* Open the AWS management console.
* Create a GitHub PAT (personal access token)
* Open the secrets manager and create a new secret named `github-pat-token` 
* Create a new secret value with key `token` within the secret `github-pat-token` and store the GitHub PAT created as the value.
* Create a new stack named `grails-multi-apps-pipeline-stack` (via Services -> Cloud Formation) using the `codepipeline/pipeline-stack.yaml` file.
* Open the code pipelines in the AWS management console and click on the `Release Change` button.
* This will execute the pipeline and create a new stack with a Fargate ECS cluster
* A new stack named `grails-multi-apps-stack` will appear (check Services -> Cloud Formation)
* When you open the new stack and view the `Oputputs` section, there are URLs for each application
* Test each url in the browser and it'll render the application