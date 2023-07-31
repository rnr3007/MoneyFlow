function loadChart(totalCostByDate) {
    loadCostByDateChart(totalCostByDate);
}

function loadCostByDateChart(totalCostByDate) {
    const labels = [];
    const myData = [];
    for (let i = 6; i >= 0; i--) {
        const dateSubtract = 24 * 3600 * 1000 * i;
        const date = new Date(new Date().setHours(0, 0, 0, 0) - dateSubtract + (24 * 3600 * 1000)).toISOString();
        labels.push(date);
        const foundData = totalCostByDate.find(x => 
            new Date(new Date(x.XAxis).getTime() + (24 * 3600 * 1000)).toISOString() == date
        );
        if (foundData) {
            myData.push(foundData.YAxis);
        } else {
            myData.push(0);
        }
    }

    const ctx = document.getElementById('lastWeekExpensesChartId');

    new Chart(ctx, {
        type: 'line',
        data: {
            labels,
            datasets: [
                {
                    label: 'Pengeluaran',
                    data: myData
                }
            ]
        },
    })
}