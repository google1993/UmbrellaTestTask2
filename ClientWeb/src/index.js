document.body.addEventListener('htmx:afterOnLoad', function(event) {
            if (event.detail.target.id === 'report-container') {
                try {
                    const jsonData = JSON.parse(event.detail.xhr.responseText);

                    console.log(jsonData)
                    createReportFromData(jsonData);
                    
                } catch (e) {
                    console.error('Ошибка обработки данных:', e);
                }
            }
        });

        function createReportFromData(data) {
    try {
        console.log('Создание отчета Stimulsoft...');

        var report = new Stimulsoft.Report.StiReport();

        report.loadFile("/reports/SimpleList.mrt");

        var dataSet = new Stimulsoft.System.Data.DataSet("Demo");
        
        var jsonData = {
            "Users": data
        };
        
        dataSet.readJson(JSON.stringify(jsonData));

        report.regData("Demo", "Demo", dataSet);

        report.render();

        var options = new Stimulsoft.Viewer.StiViewerOptions();
        var viewer = new Stimulsoft.Viewer.StiViewer(options, "StiViewer", false);
        viewer.report = report;

        var container = document.getElementById("report-container");
        if (container) {
            container.innerHTML = "";
            viewer.renderHtml("report-container");
            console.log('Отчет успешно отображен');
        }

    } catch (error) {
        console.error('Ошибка при создании отчета:', error);
        console.error('Стек ошибки:', error.stack);
    }
}
