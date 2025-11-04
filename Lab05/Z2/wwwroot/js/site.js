window.addEventListener("load", function() {
    let n = parseInt(window.prompt("Podaj rozmiar n tabliczki mnożenia (5 <= n <=20): "));

    if (isNaN(n)) {
        const p = document.getElementById("tableInfo");
        p.innerText = "Wprowadzony rozmiar nie jest liczbą.";
    } else {
        function validateN(n) {
            const p = document.getElementById("tableInfo");
            if (n < 5 || n > 20) {
                p.innerText = "Wprowadzony rozmiar jest nieprawidłowy. Zastosowano: n = 10.";
                return 10;
            } else {
                p.innerText = "Wprowadzony rozmiar jest prawidłowy. Zastosowano: n = " + n + ".";
                return n;
            }
        }

        n = validateN(n);

        function createTable(n) {
            const headerValues = [];

            const tabWrap = document.getElementById("tableWrapper");

            const table = document.createElement("table");
            const thead = document.createElement("thead");
            const trHead = document.createElement("tr");
            for (let i = 0; i <= n; i++) {
                const th = document.createElement("th");
                if (i != 0) {
                    const value = Math.floor(Math.random() * 99) + 1;
                    th.textContent = value;
                    headerValues.push(value);
                }
                trHead.appendChild(th);
            }
            thead.appendChild(trHead);
            table.appendChild(thead);
    
            const tbody = document.createElement("tbody");

            for (let i = 0; i < n; i++) {
                const tr = document.createElement("tr");
                for (let j = 0; j <= n; j++) {
                    if (j == 0) {
                        const th = document.createElement("th");
                        th.textContent = headerValues[i];
                        tr.appendChild(th);
                    } else {
                        const td = document.createElement("td");
                        let content = headerValues[i] * headerValues[j - 1];
                        td.textContent = content;
                        if (content % 2 != 0) {
                            td.className = "odd";
                        } else {
                            td.className = "even";
                        }
                        tr.appendChild(td);
                    }
                }
                tbody.appendChild(tr);
            }

            table.appendChild(tbody);
            tabWrap.appendChild(table);
        }
        createTable(n);
    }
});

window.addEventListener("load", function() {
    function draw(canvas) {
        const ctx = canvas.getContext("2d");

        function resizeCanvas() {
            canvas.width = canvas.clientWidth;
            canvas.height = canvas.clientHeight;
            ctx.strokeStyle = "green";
            ctx.lineWidth = 1;
        }
        
        function drawInner(e) {
            const width = canvas.clientWidth;
            const height = canvas.clientHeight;
            const mouseX = e.offsetX;
            const mouseY = e.offsetY;

            ctx.clearRect(0, 0, width, height);

            ctx.beginPath();

            ctx.moveTo(0, mouseY);
            ctx.lineTo(mouseX, mouseY);

            ctx.moveTo(mouseX, 0);
            ctx.lineTo(mouseX, mouseY);

            ctx.moveTo(mouseX, height);
            ctx.lineTo(mouseX, mouseY);

            ctx.moveTo(width, mouseY);
            ctx.lineTo(mouseX, mouseY);

            ctx.setLineDash([5, 5]);
            ctx.arc(mouseX, mouseY, 20, 0, 2*Math.PI);

            ctx.stroke();
        }

        resizeCanvas();
        canvas.addEventListener("mousemove", drawInner);
        canvas.addEventListener("mouseleave", () => ctx.clearRect(0, 0, canvas.clientWidth, canvas.clientHeight));
        window.addEventListener("resize", resizeCanvas);
    }
    
    const canvases = document.querySelectorAll(".drawingX");
    canvases.forEach(canvas => draw(canvas));
});