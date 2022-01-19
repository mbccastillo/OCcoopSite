@ModelType List(Of JobPostings.Models.Entities.Jobs.JobType)

@For Each i In Model
    @<li class="dropdown-header">@i.Description</li>
    @For Each j In i.NextJobCategories
        @<li>
            <a>
                <label class="plain-label">
                    <input type="checkbox" name="SelectedCategories" value='@j.CatgeroryId,@j.JobNumber' />
                    @j.Catgerory.Description # @j.JobNumber
                </label>
            </a>
        </li>
    Next

Next


        


 

