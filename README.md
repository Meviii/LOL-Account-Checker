# LoL Account Checker

This program is a League of Legends account checker with multiple features including the ability to run checks on multiple accounts, export all account information to a folder, and display comprehensive details about each account, including purchase dates and more.

## Core Features

- Run checks on multiple accounts (from text file)  
- Exports all accounts information to folder  
- Comprehensive details on each successfully checked account, including purchase dates, owned champions and skins, and more.  

## How it works
The program creates a Riot client to authenticate the user and a League client to authorize requests. It then performs a series of requests to gather account-related data.

## Requirements
- An installation of Riot/League  
- A League of Legends account

## Not Yet Implemented
- Opening and disenchanting of Hextech loot  
- Claiming event rewards and spending event tokens  
- Custom exports  
- Multi-Threading

## Application & Usage

*Note: Multi-threading currently not supported.*

### Main

The main process that runs the whole account checking operation. It includes an option to choose the combo list which is the account text file (typically looks like {username}{delimiter}{password} eg. user:pass). The delimiter is the character that splits the username and password. 

### Settings

Must be set up before being granted access to the main view. It includes the "RiotClientServices.exe location" and "LeagueClient.exe location" paths, as well as an option to update skins and champions to keep the checker up-to-date with new releases.

### Accounts

Lists all the accounts in the Exports folder in a grid view that displays general information about each account. Clicking on a specific row will open a view with more details about the account.

### Single Account

Lists the overview information of the specific account, including the purchase history, owned champions, and skins. This view also displays specific details about each owned champion and skin.
