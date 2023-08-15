function changePage(element, pageNum) {
    if (!element.classList.contains('disabled')) {
        const query = window.location.search;
        const params = new URLSearchParams(query);
        params.set('page', pageNum);
        const newUrl = `${window.location.origin}${window.location.pathname}?${params.toString()}`;

        window.location.href = newUrl;
    }
}

function changeOrder(order) {
    const query = window.location.search;
    const params = new URLSearchParams(query);
    params.set('order', order);
    const newUrl = `${window.location.origin}${window.location.pathname}?${params.toString()}`;
    window.location.href = newUrl;
}

function changeFilter(element, filter) {
    const query = window.location.search;
    const params = new URLSearchParams(query);
    let filters = params.get("filters");
    let filterList = [];
    try {
        filterList = JSON.parse(filters);
        if (filterList.length == 0) {
            throw new Error();
        }
    } catch (e) {
        filterList = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    }
    if (!element.checked) {
        filterList = filterList.filter(x => x != filter) ;
        console.log(filterList);
    } else {
        filterList.push(filter);
    }
    params.set("filters", JSON.stringify(filterList));
    const newUrl = `${window.location.origin}${window.location.pathname}?${params.toString()}`;
    window.location.href = newUrl;
}