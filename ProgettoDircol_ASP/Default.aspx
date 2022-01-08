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

    <div id="slider_vestiti" class="carousel slide carouselStile" data-ride="carousel">
        <ol class="carousel-indicators">
            <li data-target="#slider_vestiti" data-slide-to="0" class="active"></li>
            <li data-target="#slider_vestiti" data-slide-to="1"></li>
            <li data-target="#slider_vestiti" data-slide-to="2"></li>
            <li data-target="#slider_vestiti" data-slide-to="3"></li>
            <li data-target="#slider_vestiti" data-slide-to="4"></li>
        </ol>
        <div class="carousel-inner">
            <!-- Immagine 1 -->
            <div class="carousel-item active">
                <img class="d-block img-fluid" src="Images/armadio1.jpg" alt="Slide1" width="100%">

                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Caption per la slide 1</h3>
                    <p>Descrizione slide 1</p>
                </div>
            </div>
            <!-- Immagine 2 -->
            <div class="carousel-item">
                <img class="d-block img-fluid" src="Images/hm.jpg" alt="Slide1" width="100%">

                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Caption per la slide 2</h3>
                    <p>Descrizione slide 2</p>
                </div>
            </div>
            <!-- Immagine 3 -->
            <div class="carousel-item ">
                <img class="d-block w-100" src="Images/scarpe.jpg" alt="Second slide">
                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Caption per la slide 3</h3>
                    <p>Descrizione slide 3</p>
                </div>
            </div>
            <!-- Immagine 4 -->
            <div class="carousel-item ">
                <img class="d-block w-100" src="Images/armadio2.jpg" alt="Third slide">
                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Caption per la slide 4</h3>
                    <p>Descrizione slide 4</p>
                </div>
            </div>
            <!-- Immagine 5 -->
            <div class="carousel-item">
                <img class="d-block w-100" src="Images/wardrobe.jpg " alt="Slide5" width="100%">

                <div class="carousel-caption d-none d-md d-md-block">
                    <h3>Caption per la slide 5</h3>
                    <p>Descrizione slide 5</p>
                </div>
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

            <p class="lead">Cambia il tuo modo di vestirti! Rivitalizza il tuo guardaroba!</p>
            <footer class="blockquote-footer text-white">
                <cite title="Source Title">Marco Beltrame</cite>
            </footer>
            <button class="btn btn-outline-warning text-white">Scopri di più</button>
        </div>
    </div>

    <!-- FINE JUMBOTRON -->
    <!-- Implementazione sistema di feedback -->
    <section>
        <h2>Alcune statistiche dei nostri utenti</h2>
    </section>

</asp:Content>
