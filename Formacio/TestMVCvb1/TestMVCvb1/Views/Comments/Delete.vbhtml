@ModelType TestMVCvb1.TB_Comments
@Code
    ViewData("Title") = "Delete"
End Code

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<div>
    <h4>TB_Comments</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Comment)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Comment)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ParentType)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ParentType)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.ParentId)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.ParentId)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Delete" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>
