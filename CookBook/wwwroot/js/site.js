// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let dropdown = $('#recipes-dropdown');

dropdown.empty();

dropdown.append('<option selected="true" disabled>Choose Recipe</option>');
dropdown.prop('selectedIndex', 0);

const url = '/api/recipes';

// Populate dropdown with list of provinces
$.getJSON(url, function (data) {
    $.each(data, function (key, entry) {
        dropdown.append($('<option></option>').attr('value', entry.id).text(entry.name));
    });
});

$('#recipes-dropdown').on(
    'change',
    function () {
        let recipeId = $(this).val();
        let tableBody = $('#ingredientsList');
        tableBody.empty();
        $.getJSON('/api/ingredients/' + recipeId, function (data) {
            $.each(data, function (key, entry) {
                let checked = entry.isChecked === true ? "checked" : "";
                tableBody.append($('<tr><td>' + entry.name + '</td><td><input type="checkbox" ' + checked + ' class="ingredientChkBx"/> Checked</td></tr>').attr('data-id', entry.id).attr('data-value', entry.name));
            });
        });
    }
);

$('#insertIngredient').on(
    'click',
    function () {
        let ingredient = $('#newIngredient');
        let id = $('#ingredientsList li').length;
        if (ingredient !== "" && ingredient !== null) {
            $('#ingredientsList').append($('<li></li>')
                .html(ingredient.val() + '<span class="badge badge-primary badge-pill remove-ingredient">Remove</span>')
                .addClass('list-group-item')
                .attr('data-id', id)
                .attr('data-value', ingredient.val()));
            ingredient.val("");
            ingredient.focus();
        }
    }
);

$('#ingredientsList').on(
    'click',
    '.remove-ingredient',
    function () {
        let row = $(this).closest("li").data('id');
        $("ul#ingredientsList > li[data-id=" + row + "]").remove();
    }
);

$('#saveRecipe').on(
    'click',
    function (e) {
        let ingredients = [];
        $("ul#ingredientsList > li").each(function () {
            //push element data to the array
            ingredients.push({ "name": $(this).text() });
        });

        let recipeData = { name: "", Ingredients: [] };
        recipeData.name = $('#recipeName').val();
        recipeData.Ingredients = ingredients;

        $.ajax({
            url: "/api/recipes/",
            type: "POST",
            data: JSON.stringify(recipeData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                location.href = "/home";
            },
            error: function (data) {
                alert("error");
                console.log(data.responseText + jsondata);
            }
        });
    }
);

$('#ingredientsList').on(
    'click',
    '.ingredientChkBx',
    function () {
        let id = $(this).closest('tr').data('id');
        let data = { name: "", recipeId: "", isChecked: "" };
        data.name = $(this).data('value');
        if ($(this).is(':checked')) {
            data.isChecked = true;
        } else {
            data.isChecked = false;
        }
        data.recipeId = $('#recipes-dropdown').val();
        $.ajax({
            url: "/api/ingredients/" + id,
            type: "PUT",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                console.log(data);
            },
            error: function (data) {
                console.log(data);
            }
        });
    }
);