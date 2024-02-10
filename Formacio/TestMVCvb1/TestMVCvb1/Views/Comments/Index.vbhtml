@ModelType IEnumerable(Of TestMVCvb1.TB_Comments)
@Code
ViewData("Title") = "Index"
End Code

<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Comment)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ParentType)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.ParentId)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Comment)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ParentType)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.ParentId)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.CommentID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.CommentID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.CommentID })
        </td>
    </tr>
Next

</table>
