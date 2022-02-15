# Chocoholics Anonymous System

## Overview

The software system will consist of a couple major functions. 
First will be the ability of the providers to bill ChocAn for members using their resources. 
Next we have the ability to add, edit, and remove any services that providers may provide from the provider directory, 
After that we have the function of adding, updating, suspending (members), 
and deleting providers and members from the system. In addition to that, the system will also be putting together reports: 
Summary Report for the ChocAn managers to know which providers should be paid, 
Provider Reports to the providers of the services that they have provided on a weekly basis, 
and Member Reports for a list of services that the member may have used on a weekly basis. 
Finally we need to provide APIs to access all services provided records as part of the ChocAn system, 
Access to EFT data for the Acme accounting service to use, and finally access to the provider directory for providers & ChocAn managers.

## Commands

### Member

- `add member`, prompts add member to the its directory
- `edit member`, edit member in the its directory
- `remove member`, remove member from the its directory
- `suspend member`, suspend member in the its directory
- `list member`, list members in the its directory

### Provider

- `add provider`, adds provider to the its directory
- `edit provider`, edits provider in the its directory
- `remove provider`, removes provider from the its directory
- `list provider`, lists providers in the its directory

### Service

- `add service`, adds service to the its directory
- `edit service`, edits service in the its directory
- `remove service`, removes service from the its directory
- `list service`, lists services in the its directory

### Transaction

// TODO

### Reports

- `Summary report`, generates a summary report
- `Members report`, generates a member report
- `Provider report`, generates a provider report
- `EFT report`, generates a EFT report