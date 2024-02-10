@ModelType IEnumerable(Of TestMVCvb1.TB_Contact)
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
            @Html.DisplayNameFor(Function(model) model.Name)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Surname)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Surname2)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Telephone)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.e_mail)
        </th>
        <th></th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Name)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Surname)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Surname2)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.Telephone)
        </td>
        <td>
            @Html.DisplayFor(Function(modelItem) item.e_mail)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", New With {.id = item.ContactID }) |
            @Html.ActionLink("Details", "Details", New With {.id = item.ContactID }) |
            @Html.ActionLink("Delete", "Delete", New With {.id = item.ContactID }) |
            @Html.ActionLink("Comments", "Comments/Index", New With {.id = item.ContactID})
        </td>
    </tr>
Next

</table>
