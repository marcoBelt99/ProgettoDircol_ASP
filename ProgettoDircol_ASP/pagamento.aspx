<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="pagamento.aspx.cs" Inherits="ProgettoDircol_ASP.pagamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <header>
        <style>
            /* body {
                background: #f5f5f5
            }*/

            .rounded {
                border-radius: 1rem
            }

            .nav-pills .nav-link.active {
                background-color: #7952b3;
            }

            .nav-pills .nav-link {
                color: #555
            }

                .nav-pills .nav-link.active {
                    color: white;
                    /* background-color: #7952b3;*/
                }

            input[type="radio"] {
                margin-right: 5px
            }

            .bold {
                font-weight: bold
            }
        </style>
        <%-- CSS di Bootstrap --%>
        <%--<link rel="stylesheet" href="	https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />--%>
        <%-- Icone --%>
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" />
        <%-- Javascript di Bootstrap --%>
        <script href="https://stackpath.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.bundle.min.js"></script>
        <%-- JQuery --%>
        <script href="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    </header>

    <asp:Panel ID="pnPagamento" runat="server">



        <center>
            <div class="form-row">
                <div class="col">
                    <label for="formGroupExampleInput">Totale Prezzo di Listino</label>
                </div>
                <div class="col">
                    <asp:TextBox CssClass="form-control" ID="txtTotaleListino" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="col">
                    <label for="formGroupExampleInput">Costo del trasporto</label>
                </div>
                <div class="col">
                    <asp:TextBox CssClass="form-control" ID="txtPrezzoTrasporto" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="col">
                    <label for="formGroupExampleInput">IVA</label>
                </div>
                <div class="col">
                    <asp:TextBox CssClass="form-control" ID="txtIVA" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-row">
                <div class="col">
                    <label for="formGroupExampleInput">Prezzo totale di vendita</label>
                </div>
                <div class="col">
                    <asp:TextBox CssClass="form-control" ID="txtTotaleVendita" runat="server"></asp:TextBox>
                </div>
            </div>
        </center>

        <div class="container py-5">
            <!-- For demo purpose -->
            <div class="row mb-4">
                <div class="col-lg-8 mx-auto text-center">
                    <h1 class="display-6">Modulo di pagamento</h1>
                </div>
            </div>
            <!-- End -->
            <div class="row">
                <div class="col-lg-6 mx-auto">
                    <div class="card ">
                        <div class="card-header">
                            <div class="<%--bg-white--%> shadow-sm pt-4 pl-2 pr-2 pb-2">
                                <!-- Credit card form tabs -->
                                <ul role="tablist" class="nav <%--bg-light--%> nav-pills rounded nav-fill mb-3">
                                    <li class="nav-item"><a data-toggle="pill" href="#credit-card" class="nav-link active"><i class="fas fa-credit-card mr-2"></i>Carta di credito</a></li>
                                    <li class="nav-item"><a data-toggle="pill" href="#paypal" class="nav-link "><i class="fab fa-paypal mr-2"></i>Paypal </a></li>
                                    <li class="nav-item"><a data-toggle="pill" href="#net-banking" class="nav-link "><i class="fas fa-mobile-alt mr-2"></i>Home Banking</a></li>
                                </ul>
                            </div>
                            <!-- End -->
                            <!-- CARTA DI CREDITO -->
                            <div class="tab-content">
                                <!-- credit card info-->
                                <div id="credit-card" class="tab-pane fade show active pt-3">
                                    <%-- <form role="form" onsubmit="event.preventDefault()">--%>
                                    <div class="form-group">
                                        <label for="username">
                                            <h6>Proprietario della carta</h6>
                                        </label>
                                        <input type="text" name="username" placeholder="Nome del proprietario della carta" required class="form-control ">
                                    </div>
                                    <div class="form-group">
                                        <label for="cardNumber">
                                            <h6>Numero di carta</h6>
                                        </label>
                                        <div class="input-group">
                                            <input type="text" name="cardNumber" placeholder="Inserisci un numero di carta valido" class="form-control " required>
                                            <div class="input-group-append"><span class="input-group-text text-muted"><i class="fab fa-cc-visa mx-1"></i><i class="fab fa-cc-mastercard mx-1"></i><i class="fab fa-cc-amex mx-1"></i></span></div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <div class="form-group">
                                                <label>
                                                    <span class="hidden-xs">
                                                        <h6>Data di scadenza</h6>
                                                    </span>
                                                </label>
                                                <div class="input-group">
                                                    <input type="number" placeholder="MM" name="" class="form-control" required>
                                                    <input type="number" placeholder="YY" name="" class="form-control" required>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group mb-4">
                                                <label data-toggle="tooltip" title="Three digit CV code on the back of your card">
                                                    <h6>CVV <i class="fa fa-question-circle d-inline"></i></h6>
                                                </label>
                                                <input type="text" required class="form-control">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <asp:Button CssClass="subscribe btn btn-success btn-block shadow-sm"
                                            ID="btnConfermaPagamento"
                                            runat="server"
                                            Text="Conferma il pagamento"
                                            OnClick="btnConfermaPagamento_Click"></asp:Button>
                                        <%-- </form>--%>
                                    </div>
                                </div>
                                <!-- End -->
                                <!-- PAYPAL -->
                                <div id="paypal" class="tab-pane fade pt-3">
                                    <h6 class="pb-2">Seleziona il tuo account Paypal</h6>
                                    <div class="form-group ">
                                        <label class="radio-inline">
                                            <input type="radio" name="optradio" checked>
                                            Domestico
                                        </label>
                                        <label class="radio-inline">
                                            <input type="radio" name="optradio" class="ml-5">Internazionale
                                        </label>
                                    </div>
                                    <p>
                                        <button type="button" class="btn btn-primary "><i class="fab fa-paypal mr-2"></i>Accedi a Paypal</button>
                                    </p>
                                </div>
                                <!-- End -->
                                <!-- BANCA -->
                                <div id="net-banking" class="tab-pane fade pt-3">
                                    <div class="form-group ">
                                        <label for="Seleziona la tua banca">
                                            <h6>Seleziona la tua banca</h6>
                                        </label>
                                        <select class="form-control" id="ccmonth">
                                            <option value="" selected disabled>--Seleziona la tua banca--</option>
                                            <option>Intesa San Paolo</option>
                                            <option>Monte dei Paschi di Siena</option>
                                            <option>Carige</option>
                                            <option>Unicredit SpA</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <p>
                                            <button type="button" class="btn btn-primary "><i class="fas fa-mobile-alt mr-2"></i>Procedi al pagamento</button>
                                        </p>
                                    </div>
                                </div>
                                <!-- End -->
                                <!-- End -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script>
            $(function () {
                $('[data-toggle="tooltip"]').tooltip()
            })

            //$(".nav-pills").css("background-color:", "#7952b3");
            $("a.nav-link.active").css("background-color:", "#7952b3");
                //$(".nav-pills").css("background-color:", "#7952b3");
        </script>
    </asp:Panel>

</asp:Content>
