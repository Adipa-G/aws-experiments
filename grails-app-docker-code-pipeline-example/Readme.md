# AWS Code Pipelines

This folder contains an AWS code pipeline to deploy a Grails application,

The pipeline has following steps:

* Cheking out from GitHub repo
* Build Grails application
* Self update the code pipeline
* Create / Update the ECS cluster running the Application

## Setting up

* Fork this repository.
* Open the `/codepipeline/pipeline-stack.yaml` file and update the text `Owner: Adipa-G` to `Owner: <your github organisation name, and ensure to use the same case>`
* Commit the file `/codepipeline/pipeline-stack.yaml`
* Open the AWS management console.
* Create a GitHub PAT (personal access token)
* Open the secrets manager and create a new secret named `github-pat-token` 
* Create a new secret value with key `token` within the secret `github-pat-token` and store the GitHub PAT created as the value.
* Create a new stack named `grails-app-pipeline-stack` (via Services -> Cloud Formation) using the `codepipeline/pipeline-stack.yaml` file.
* Open the code pipelines in the AWS management console and click on the `Release Change` button.
* This will execute the pipeline and create a new stack with a Fargate ECS cluser
* A new stak named `grails-app-stack` will appear (check Services -> Cloud Formation)
* When you open the new stack and view the `Oputputs` section, there is the URL for the new application
* Test the APP in the browser and it'll render the application