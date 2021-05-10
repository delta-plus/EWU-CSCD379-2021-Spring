
# Assignment 6

![](../../workflows/Build/badge.svg)
![](../../workflows/Deploy/badge.svg)

My branch: https://github.com/delta-plus/EWU-CSCD379-2021-Spring/tree/Assignment6

My site: https://secretsantaserver.azurewebsites.net

## Overview

In this project, we are going to configure continuous integration and continuous deployment.

## Assignment

- Configure a CI Build against the SecretSanta solution
  - Compile all the source code ✔❌
  - Run all the user tests ✔❌
  - Add the Build Status to the Read.me file of your project.  (Be sure to use relative links so it works across branches and even when forked.) ✔❌
  - Place a URL to your fork and branch in your readme (so that we can easily navigate there to see the build)
- Configure a CD deployment of the project (https://docs.microsoft.com/en-us/azure/app-service/deploy-github-actions)
  - Configure an Azure Web App (using the Azure Portal) ✔❌
  - Configure a GitHub Action to deploy to an Azure Web App (https://github.com/Azure/webapps-deploy) ✔❌
  - Add the Deploy Status to the Read.me file of your project.  (Be sure to use relative links so it works across branches and even when forked.) ✔❌
  - Be sure to deploy the API project and verify that the UI that calls the API project works correctly. This will require you to update the URL/address in the Web project that points to the API project. ✔❌
- Place a hyperlink to the running website in both your Readme and your PR. (Consider using a badge that shows whether the site is up or not.) ✔❌

## Extra Credit

- Have the Web Tests run against the deployed website after the deployment completes.
- Configure the API site URL address into the Web Project during the deployment using a GitHub Action variable.
- Configure code coverage and generate a report (see https://github.com/dotnet/docs/blob/main/docs/core/testing/unit-testing-code-coverage.md) ✔❌
