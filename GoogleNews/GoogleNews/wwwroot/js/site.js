
// Function to update post content
const updatePostContent = (title, description, link) => {

    //Take element from DOM.
    const titleElement = document.getElementById("title");
    const descriptionElement = document.getElementById("description");
    const linkElement = document.getElementById("link");
    const close = document.getElementById("close")

    //Set elements.
    close.textContent = "X"

    //Add function on click.
    //The function clear all items of title.
    close.addEventListener("click", function () {
        close.textContent='';
        titleElement.textContent = '';
        descriptionElement.innerHTML = '';
        linkElement.href = '';
        linkElement.textContent = '';
    })

    titleElement.textContent = title;
    descriptionElement.innerHTML = description;
    linkElement.href = link;
    linkElement.textContent = "Read more";
}

// Function to fetch news content.
const getNews = (title) => {
    //Use Ajax to call. 
    $.ajax({
        type: "GET",
        url: "/GoogleNews/GetItem/" + title,
        data: { title: title },
        dataType: "json",
        success: function (data) {
            updatePostContent(data.title, data.body, data.link);
        },
        error: function () {
            console.log("error fetching")
        }
    });
}

// Function to fetch news titles.
const getTitles = () => {
    //Use Ajax to call. 
    $.ajax({
        type: "GET",
        url: "/GoogleNews/GetAllNews",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        //Use JQuery and JS instead of repeater control.
        success: function (data) {
            const ul = document.getElementById("Titles");
            Titles = $("#Titles");
            data.forEach(item => {
                var newsItemHtml = '<a href="#" class="title" data-title="' + item.title + '">' + item.title + '</a>';
                Titles.append(newsItemHtml);
            });

            //Add event listener.
            //On click return all data of title.
            Titles.on('click', '.title', function () {
                getNews($(this).closest('.title').data('title'));
            });
        },
        error: function () {
            alert("Error fetching news.");
        }
    });
}

//In load html the function getTitles action.
window.onload = getTitles;