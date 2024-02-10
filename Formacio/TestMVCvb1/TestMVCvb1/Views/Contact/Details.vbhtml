@ModelType TestMVCvb1.TB_Contact
@Code
    ViewData("Title") = "Details"
End Code

<h2>Details</h2>

<div>
    <h4>TB_Contact</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Name)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Name)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Surname)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Surname)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Surname2)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Surname2)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Telephone)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Telephone)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.e_mail)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.e_mail)
        </dd>

    </dl>
</div>
<p>
    @Html.ActionLink("Edit", "Edit", New With { .id = Model.ContactID }) |
    @Html.ActionLink("Back to List", "Index")
</p>
