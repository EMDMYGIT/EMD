@ModelType TestMVCvb1.TB_Comments
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

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
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.CommentID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
