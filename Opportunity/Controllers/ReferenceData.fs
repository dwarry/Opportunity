module Opportunity.ReferenceData

open Opportunity.DataTransferObjects

open System.Linq

let Categories = Opportunity.Domain.DataAccess.getCategories ()
                 |> Seq.map (fun c -> { Category.Id = c.Id; Name = c.Name; Icon = c.Icon } )
                 |> Seq.toArray

let private fixupOrgUnitTree () =
    let orgUnits = Opportunity.Domain.DataAccess.getOrgUnits ()
    let byParent = Enumerable.ToLookup(orgUnits, fun ou -> ou.ParentId)
    let itemsWithParent parentId = if byParent.Contains parentId then byParent.[parentId] else Seq.empty
    let rec toDtos parentId = itemsWithParent parentId 
                              |> Seq.map (fun ou -> {
                                                        OrganizationalUnit.Id = ou.Id
                                                        Name = ou.Name
                                                        Colour = ou.Colour
                                                        Icon = ou.Icon
                                                        SubOrgUnits = toDtos (Some ou.Id)
                                                    })    
                              |> Seq.toArray   
    toDtos None

let OrgUnits = fixupOrgUnitTree ()

let RefData = { Categories = Categories; OrgUnits = OrgUnits}
