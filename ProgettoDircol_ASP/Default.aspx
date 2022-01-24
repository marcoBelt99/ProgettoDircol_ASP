<%@ Page Title="HomePage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProgettoDircol_ASP._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <%--<p class="provaFunzionamento">provaClasse</p>--%>
    <!-- PAGINA DI PRESENTAZIONE -->

    <%-- Includo lo stile per il testo personalizzato --%>
    <link rel="stylesheet" href="~/Content/testo.css" />
    <%-- Inserisco lo stile per il testo personalizzato --%>
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Poppins:wght@700&display=swap');

        .testoAnimato {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Poppins', sans-serif;
        }

            .testoAnimato div {
                display: flex;
                justify-content: center;
                align-items: center;
                min-height: 100vh;
                background: #fff;
            }

            .testoAnimato h1 {
                position: relative;
                font-size: 14vw;
                color: #fff;
                -webkit-text-stroke: 0.3vw rgb(190, 190, 190); /* #383d52 */
                text-transform: uppercase;
                border-color: #dd00ae;
            }

                .testoAnimato h1::before {
                    content: attr(data-text);
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 0;
                    height: 100%;
                    color: #ffc107;
                    -webkit-text-stroke: 0vw #383d52;
                    border-right: 2px solid #ffc107;
                    border-color: #dd00ae;
                    overflow: hidden;
                    animation: animate 6s linear infinite;
                }

        @keyframes animate {
            0%, 10%, 100% {
                width: 0;
            }

            70%, 90% {
                width: 100%;
            }
        }
    </style>
    <!-- CAROUSEL -->

    <div id="slider_vestiti" class="carousel slide carouselStile" data-ride="carousel" data-interval="1500">
        <ol class="carousel-indicators">
            <li data-target="#slider_vestiti" data-slide-to="0" class="active"></li>
            <li data-target="#slider_vestiti" data-slide-to="1"></li>
            <li data-target="#slider_vestiti" data-slide-to="2"></li>
            <li data-target="#slider_vestiti" data-slide-to="3"></li>
            <li data-target="#slider_vestiti" data-slide-to="4"></li>
            <li data-target="#slider_vestiti" data-slide-to="5"></li>
            <li data-target="#slider_vestiti" data-slide-to="6"></li>
            <li data-target="#slider_vestiti" data-slide-to="7"></li>
            <li data-target="#slider_vestiti" data-slide-to="8"></li>
            <li data-target="#slider_vestiti" data-slide-to="9"></li>
            <li data-target="#slider_vestiti" data-slide-to="10"></li>
        </ol>
        <div class="carousel-inner">
            <!-- Immagine 1 -->
            <div class="carousel-item active">
                <img class="d-block img-fluid" src="Images/camp25.jpg" alt="Slide1" width="100%" heigt="200px">
                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Dircol</h3>
                    <p>Il tuo abbigliamento, subito</p>
                </div>
            </div>
            <!-- Immagine 2 -->
            <div class="carousel-item">
                <img class="d-block img-fluid" src="Images/camp26.jpg" alt="Slide1" width="100%" heigt="200px">

                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Dircol</h3>
                    <p>Rivitalizza il tuo guardaroba!</p>
                </div>
            </div>
            <!-- Immagine 3 -->
            <div class="carousel-item">
                <img class="d-block img-fluid" src="Images/camp10.jpg" alt="Slide1" width="100%" heigt="200px">

                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Dircol</h3>
                    <p>Il player numero 1 nel mercato dell'abbigliamento italiano.</p>
                </div>
            </div>
            <!-- Immagine 4 -->
            <div class="carousel-item ">
                <img class="d-block w-100" src="Images/camp14.jpg" alt="Third slide" width="100%" heigt="200px">
            </div>
            <!-- Immagine 5 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/camp15.jpg" alt="Slide5" width="100%" heigt="200px">
            </div>
            <!-- Immagine 6 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/camp16.jpg" alt="Slide5" width="100%" heigt="200px">
            </div>
            <!-- Immagine 7 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/camp19.jpg" alt="Slide5" width="100%" heigt="200px">
            </div>
            <!-- Immagine 8 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/camp20.jpg" alt="Slide5" width="100%" heigt="200px">
            </div>
            <!-- Immagine 9 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/camp21.jpg" alt="Slide5" width="100%" heigt="200px">
            </div>
            <!-- Immagine 10 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/camp36.jpg" alt="Slide5" width="100%" heigt="200px">
            </div>
            <!-- Immagine 11 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/camp7.jpg" alt="Slide5" width="100%" heigt="200px">
            </div>
        </div>
        <a class="carousel-control-prev" href="#slider_vestiti" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
        </a>
        <a class="carousel-control-next" href="#slider_vestiti" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
        </a>
    </div>

    <div style="margin-bottom: 2px;"></div>
    <!-- FINE CAROUSEL -->


    <!-- JUMBOTRON -->
    <div class="jumbotron jumbotron-fluid text-white">
        <div class="container">
            <!-- <h1 class="display-1">Dircol s.p.a.</h1> -->
            <div class="testoAnimato">
                <h1 class="display-1" data-text="Dircol">Dircol</h1>
            </div>

            <p class="lead">Cambia il tuo modo di vestirti! </p>
            <footer class="blockquote-footer text-white">
                <cite title="Source Title">Marco Beltrame</cite>
            </footer>
            <a id="linkChiSiamo" 
                runat="server"
                class="btn btn-outline-warning text-white"
                role="button"
                href="chiSiamo.aspx"
                >Scopri di più</a>
        </div>
    </div>

    <!-- FINE JUMBOTRON -->
    <!-- Implementazione sistema di feedback -->
    <section>
        <h2>Alcune statistiche dei nostri utenti</h2>
    </section>

</asp:Content>
