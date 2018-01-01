# WheelOfFate

## Summary

It an application for picking up some amount of engineers for doing support. 
It is a kinde of training project to get knowledhe and experince. 

### Stack

Thechnicaly it is a .NET Core WebAPI on back-end and React + Typescript on front-end. 
It is built on layered architecture: API controller + Business Domain Services + DAL repositories.
As DAL back-end it uses Entity Framework Core.

## Background

At one company, all engineers take it in turns to support the business for half a day at a time. This is affectionately known as BAU.
Currently, there is no tool which decides who is doing BAU and when, all rotas are created and maintained by hand.

## Business Rules

There are some rules and these are liable to change in the future:
- An engineer can do at most one half day shift in a day.
- An engineer cannot have half day shifts on consecutive days.
- Each engineer should have completed one whole day of support in any 2 week period.

