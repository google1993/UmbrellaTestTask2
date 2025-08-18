let currentReport = null;

document.addEventListener("DOMContentLoaded", function() {
	const tabs = document.querySelectorAll('#reportTabs button[data-bs-toggle="tab"]');
	tabs.forEach(tab => {
		tab.addEventListener('shown.bs.tab', function(event) {
			const targetId = event.target.getAttribute('data-bs-target').substring(1);

			const reportResult = document.getElementById("reportResult");
			reportResult.innerHTML = '<div class="loading"><div class="spinner-border" role="status"></div></div>';

			if (targetId === "tests") {
				document.body.dispatchEvent(new CustomEvent('load-tests-report'));
			} else if (targetId === "users") {
				document.body.dispatchEvent(new CustomEvent('load-users-report'));
			} else if (targetId === "errors") {
				reportResult.innerHTML = '';
			}
		});
	});

	document.body.dispatchEvent(new CustomEvent('load-users-report'));
});

document.getElementById('errorsForm').addEventListener('submit', async function(e) {
	e.preventDefault();

	const formData = new FormData(this);
	const startMonth = formData.get('monthFrom');
	const endMonth = formData.get('monthTo');
	const api = "http://127.0.0.1:5000";

	try {
		const reportResult = document.getElementById("reportResult");
		reportResult.innerHTML = '<div class="loading"><div class="spinner-border" role="status"></div></div>';

		const isoStartDate = new Date(startMonth + "-01").toISOString();
		const isoEndDate = new Date(new Date(endMonth + "-01").getFullYear(), new Date(endMonth + "-01").getMonth() + 1, 0).toISOString();

		const reportData = {};

		const pieResp = await fetch(api + "/error/get_list_errors_pie", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({
				StartDate: isoStartDate,
				EndDate: isoEndDate
			})
		});

		if (!pieResp.ok) throw new Error();
		reportData.pie_char = await pieResp.json();

		const errorsResp = await fetch(api + "/error/get_list_error", {
			method: "POST",
			headers: {
				"Content-Type": "application/json"
			},
			body: JSON.stringify({
				StartDate: isoStartDate,
				EndDate: isoEndDate
			})
		});

		if (!errorsResp.ok) throw new Error();
		reportData.errors = await errorsResp.json();

		await createReportFromData(reportData, "reportResult");

	} catch (error) {
		console.log(error)
	}
});

async function createReportFromData(data, containerId) {
	try {
		console.log(containerId);
        console.log(data);

		var report = new Stimulsoft.Report.StiReport();

		const activeTab = document.querySelector('#reportTabs .nav-link.active');
		const activeTabId = activeTab ? activeTab.id : '';

		if (activeTabId === "tests-tab") {
			reportFile = "./reports/Report.mrt";
		} else if (activeTabId === "users-tab") {
			reportFile = "./reports/main-stat-report.mrt";
		} else if (activeTabId === "errors-tab" || containerId === "reportResult") {
			reportFile = "./reports/Report2.mrt";
		}

		report.loadFile(reportFile);

		var dataSetName = "report-data";
		var dataSet = new Stimulsoft.System.Data.DataSet(dataSetName);
		dataSet.readJson(JSON.stringify(data));
		report.dictionary.databases.clear();
		report.regData(dataSetName, dataSetName, dataSet);

		await report.renderAsync();

		currentReport = report;

		var options = new Stimulsoft.Viewer.StiViewerOptions();
		var viewer = new Stimulsoft.Viewer.StiViewer(options, "StiViewer", false);
		viewer.report = report;

		var container = document.getElementById("reportResult");
		container.innerHTML = "";
		viewer.renderHtml("reportResult");
	} catch (error) {
		console.error(error);
	}
}

document.body.addEventListener('htmx:afterOnLoad', async function(event) {
	try {
		const jsonData = JSON.parse(event.detail.xhr.responseText);

		await createReportFromData(jsonData, "reportResult");

	} catch (e) {
		console.warn(e);
	}
});

window.exportToHTML = function() {
  if (!currentReport) return;
  currentReport.exportDocumentAsync(function(html) {
    Stimulsoft.System.StiObject.saveAs(html, "report.html", "text/html;charset=utf-8");
  }, Stimulsoft.Report.StiExportFormat.Html);
}

window.exportToPDF = function() {
  if (!currentReport) return;
  currentReport.exportDocumentAsync(function(pdfData) {
    Stimulsoft.System.StiObject.saveAs(pdfData, "report.pdf", "application/pdf");
  }, Stimulsoft.Report.StiExportFormat.Pdf);
}