@ModelType List(Of JobPostings.Models.Entities.Jobs.JobType)


     @For Each j In Model
         @<optgroup label='@j.Description'>
            
                @For Each c In j.NextJobCategories
                    @<option value='@c.CatgeroryId,@c.JobNumber'>@c.DisplayDescription</option>
                Next
                        
        </optgroup>
     Next


        


 

