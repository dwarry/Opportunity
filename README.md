# Opportunity

This started life as a project for a Hackathon at work - you get a day to build something using whatever technologies you like - and I figured I may as well polish it up as a portfolio piece. 

## The Brief

This is a small web application to track Opportunities - small, targetted, pieces of work for people to get involved in to broaden their experience or gain new skills. These are intended as add-ons to people's normal jobs, rather than being full-on internal job ads. There are currently various *ad hoc* systems for managing these Opportunities (typically Excel spreadsheets!) for each business area. 

Users may view the currently open Opportunities, and register their interest and apply with a short covering note. Owners of opportunities may review all the applications, score them, arrange meetings with the applicants etc.

## Technology Stack and Application Architecture

### Back End
The back-end is a SQL-Server + ASP.Net Web API site. The WebAPI is written in F#. Why F#? Mainly because I wanted to build something with it, and I thought it might be a good candidate application. I'd grown a bit frustrated with the "traditional" approach of using an ORM such as Entity Framework for WebAPI projects; it always felt slightly redundant to have a full domain model when all that would happen in a Controller method would be retrieve one or more domain instances and map them to DTOs for serializing to the client. Sometimes it felt like it took longer to write the query so that EF wouldn't generate horrible SQL than it would have to just write the SQL in the first place. So this also doubles up as an experiment in using the FSharp.Data.SqlClient Type Providers. Give the type provider a query, and let it generate the classes for to execute it, and a data structure for the results. 

### Front End
The Front End is an [Aurelia](http://aurelia.io) Single Page Application. There is one ASP.Net View that renders the minimal page and launches the javascript application. I've been using Bootstrap for the last few years, so I thought I'd try out [MaterializeCSS](http://materializecss.com) for a change, and the [Aurelia-Materialize Bridge](https://github.com/aurelia-ui-toolkits/aurelia-materialize-bridge). 

**NB** As this was originally intended to be an intranet application, it's just using Windows Integrated Authentication - no OAuth or even the usual ASP.Net Identity system. As I'm developing this on my personal laptop without an Active Directory to hand, the bits that retrieve the users data from AD will be stubbed out for now. 