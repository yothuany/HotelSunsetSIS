CREATE DATABASE BD_HotelSunset;
USE BD_HotelSunset;

CREATE TABLE TiposQuarto(
    id_tipo_quarto INT PRIMARY KEY AUTO_INCREMENT,
    nome_tip VARCHAR(50) NOT NULL UNIQUE,
    descricao_tip TEXT
);

CREATE TABLE Quartos(
    id_quarto INT PRIMARY KEY AUTO_INCREMENT,
    numero_qua VARCHAR(10) NOT NULL UNIQUE,
    status_qua VARCHAR(50) DEFAULT 'Disponível', #'Disponível', 'Ocupado', 'Manutenção', 'Limpeza'
    andar_qua INT,
    capacidade_qua INT,
    id_tipo_quarto_fk INT NOT NULL,
    FOREIGN KEY(id_tipo_quarto_fk) REFERENCES TiposQuarto(id_tipo_quarto)
);

CREATE TABLE Hospedes(
    id_hospede INT PRIMARY KEY AUTO_INCREMENT,
    nome_hos VARCHAR(100) NOT NULL,
    cpf_hos VARCHAR(14) UNIQUE NOT NULL,
    data_nascimento_hos DATE,
    email_hos VARCHAR(100),
    telefone_hos VARCHAR(20)
);

CREATE TABLE Reservas(
    id_reserva INT PRIMARY KEY AUTO_INCREMENT,
    data_checkin_res DATETIME NOT NULL,
    data_checkout_res DATETIME NOT NULL,
    status_res VARCHAR(50) DEFAULT 'Pendente', # 'Pendente', 'Confirmada', 'Cancelada'
    valor_total_res DECIMAL(10, 2),
    numero_hospedes_res INT,
    observacoes_res TEXT,
    id_hospede_fk INT NOT NULL,
    id_quarto_fk INT NOT NULL,
    FOREIGN KEY(id_hospede_fk) REFERENCES Hospedes(id_hospede),
    FOREIGN KEY(id_quarto_fk) REFERENCES Quartos(id_quarto)
);

CREATE TABLE Servicos(
    id_servico INT PRIMARY KEY AUTO_INCREMENT,
    nome_ser VARCHAR(100) NOT NULL,
    descricao_ser TEXT,
    preco_ser DECIMAL(10, 2) NOT NULL
);

CREATE TABLE ServicosConsumidos(
    id_serv_consumido INT PRIMARY KEY AUTO_INCREMENT,
    quantidade_sco INT NOT NULL DEFAULT 1,
    data_consumo_sco DATETIME,
    valor_total_sco DECIMAL(10, 2),
    id_reserva_fk INT NOT NULL,
    id_servico_fk INT NOT NULL,
    FOREIGN KEY(id_reserva_fk) REFERENCES Reservas(id_reserva) ON DELETE CASCADE,
    FOREIGN KEY(id_servico_fk) REFERENCES Servicos(id_servico)
);

CREATE TABLE Funcionarios(
    id_funcionario INT PRIMARY KEY AUTO_INCREMENT,
    nome_fun VARCHAR(100) NOT NULL,
    cpf_fun VARCHAR(14) UNIQUE NOT NULL,
    data_nascimento_fun DATE,
    rg_fun VARCHAR(12) UNIQUE,
    salario_fun DECIMAL(10, 2),
    email_fun VARCHAR(255),
    telefone_fun VARCHAR(20)
);

CREATE TABLE Caixa(
    id_caixa INT PRIMARY KEY AUTO_INCREMENT,
    valor_inicial_cai DECIMAL(10, 2) NOT NULL,
    data_abertura_cai DATETIME,
    data_fechamento_cai DATETIME,
    total_entradas_cai DECIMAL(10, 2),
    total_saidas_cai DECIMAL(10, 2),
    id_funcionario_fk INT NOT NULL,
    FOREIGN KEY(id_funcionario_fk) REFERENCES Funcionarios(id_funcionario)
);

CREATE TABLE TiposPagamento(
    id_tipo_pagamento INT PRIMARY KEY AUTO_INCREMENT,
    nome_tpg VARCHAR(50) NOT NULL UNIQUE,
    descricao_tpg TEXT
);
CREATE TABLE Vendas(
    id_venda INT PRIMARY KEY AUTO_INCREMENT,
    data_ven DATETIME,
    valor_total_ven DECIMAL(10, 2) NOT NULL,
    id_reserva_fk INT,
    id_funcionario_fk INT NOT NULL,
	id_tipo_pagamento_fk INT NOT NULL,
    FOREIGN KEY(id_reserva_fk) REFERENCES Reservas(id_reserva),
    FOREIGN KEY(id_funcionario_fk) REFERENCES Funcionarios(id_funcionario),
	FOREIGN KEY(id_tipo_pagamento_fk) REFERENCES TiposPagamento(id_tipo_pagamento)
);

CREATE TABLE Pagamentos(
    id_pagamento INT PRIMARY KEY AUTO_INCREMENT,
    valor_pag DECIMAL(10, 2) NOT NULL,
    data_pag DATETIME,
    status_pag VARCHAR(50) DEFAULT 'Pendente', # 'Pendente', 'Concluído', 'Reembolsado', 'Falhou'
    id_reserva_fk INT,
    id_caixa_fk INT NOT NULL,
    id_venda_fk INT,
	id_tipo_pagamento_fk INT NOT NULL,
    FOREIGN KEY(id_reserva_fk) REFERENCES Reservas(id_reserva) ON DELETE CASCADE,
    FOREIGN KEY(id_caixa_fk) REFERENCES Caixa(id_caixa), 
    FOREIGN KEY(id_venda_fk) REFERENCES Vendas(id_venda) ON DELETE CASCADE,
	FOREIGN KEY(id_tipo_pagamento_fk) REFERENCES TiposPagamento(id_tipo_pagamento),
    CONSTRAINT chk_origem_pagamento CHECK (id_reserva_fk IS NOT NULL OR id_venda_fk IS NOT NULL)
);

CREATE TABLE Produtos(
    id_prod INT PRIMARY KEY AUTO_INCREMENT,
    nome_pro VARCHAR(100) NOT NULL,
    descricao_pro TEXT,
    preco_pro DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Estoque(
    id_estoque INT PRIMARY KEY AUTO_INCREMENT,
    quantidade_est INT NOT NULL,
    data_validade_est DATE,
    lote_est VARCHAR(50),
    id_prod_fk INT NOT NULL,
    FOREIGN KEY(id_prod_fk) REFERENCES Produtos(id_prod)
);

CREATE TABLE ItensVenda(
    id_item_venda INT PRIMARY KEY AUTO_INCREMENT,
    quantidade_ive INT NOT NULL,
    preco_unitario_ive DECIMAL(10, 2) NOT NULL,
    id_venda_fk INT NOT NULL,
    id_prod_fk INT NOT NULL,
    FOREIGN KEY(id_venda_fk) REFERENCES Vendas(id_venda) ON DELETE CASCADE,
    FOREIGN KEY(id_prod_fk) REFERENCES Produtos(id_prod)
);

########################################################## INSERTS ########################################################################################
INSERT INTO TiposQuarto (nome_tip, descricao_tip) VALUES
('Standard', 'Quarto simples com cama de casal ou duas de solteiro.'),
('Luxo', 'Quarto espaçoso com cama king-size e vista privilegiada.'),
('Suite', 'Acomodação com área de estar separada e banheira de hidromassagem.'),
('Duplo', 'Quarto com duas camas de casal, ideal para famílias.'),
('Executivo', 'Quarto com área de trabalho e comodidades extras.'),
('Familiar', 'Quarto amplo com múltiplas camas para grupos grandes.'),
('Acessível', 'Quarto adaptado para hóspedes com necessidades especiais.'),
('Presidencial', 'Suíte de alto luxo com múltiplos cômodos e serviços exclusivos.'),
('Conectado', 'Dois quartos Standard com porta de conexão interna.'),
('Econômico', 'Quarto compacto e funcional para estadias curtas.');

INSERT INTO Quartos (numero_qua, status_qua, andar_qua, capacidade_qua, id_tipo_quarto_fk) VALUES
('101', 'Disponível', 1, 2, 1),
('102', 'Ocupado', 1, 2, 1),
('103', 'Manutenção', 1, 1, 1),
('201', 'Disponível', 2, 2, 2),
('202', 'Ocupado', 2, 2, 2),
('203', 'Disponível', 2, 3, 2),
('301', 'Disponível', 3, 4, 3),
('302', 'Ocupado', 3, 2, 3),
('303', 'Limpeza', 3, 2, 3),
('104', 'Disponível', 1, 1, 1),
('204', 'Disponível', 2, 2, 2),
('304', 'Disponível', 3, 3, 3),
('105', 'Ocupado', 1, 2, 1),
('205', 'Manutenção', 2, 2, 2),
('305', 'Disponível', 3, 4, 3);

INSERT INTO Hospedes (nome_hos, cpf_hos, data_nascimento_hos, email_hos, telefone_hos) VALUES
('Ana Silva', '111.111.111-11', '1985-03-15', 'ana.s@email.com', '91234-5678'),
('Bruno Costa', '222.222.222-22', '1990-07-20', 'bruno.c@email.com', '98765-4321'),
('Carla Dias', '333.333.333-33', '1978-11-01', 'carla.d@email.com', '99887-6655'),
('Daniel Souza', '444.444.444-44', '1995-01-25', 'daniel.s@email.com', '97654-3210'),
('Eduarda Lima', '555.555.555-55', '1982-09-10', 'eduarda.l@email.com', '96543-2109'),
('Fernando Gomes', '666.666.666-66', '1970-04-05', 'fernando.g@email.com', '95432-1098'),
('Giovana Martins', '777.777.777-77', '1998-12-30', 'giovana.m@email.com', '94321-0987'),
('Hugo Pereira', '888.888.888-88', '1988-06-18', 'hugo.p@email.com', '93210-9876'),
('Isabela Rocha', '999.999.999-99', '1993-02-14', 'isabela.r@email.com', '92109-8765'),
('Julio Santos', '101.101.101-01', '1980-08-08', 'julio.s@email.com', '91098-7654'),
('Karen Alves', '121.121.121-12', '1975-01-01', 'karen.a@email.com', '90987-6543'),
('Lucas Barbosa', '131.131.131-13', '1991-04-22', 'lucas.b@email.com', '98765-1234');

INSERT INTO Reservas (data_checkin_res, data_checkout_res, status_res, valor_total_res, 
numero_hospedes_res, observacoes_res, id_hospede_fk, id_quarto_fk) VALUES
('2025-08-01', '2025-08-05', 'Confirmada', 600.00, 10, 'Cama de casal', 1, 1),
('2025-08-10', '2025-08-12', 'Pendente', 450.00, 9, 'Silencioso', 2, 4),
('2025-07-20', '2025-07-25', 'Confirmada', 1500.00, 8, 'Vista para o mar', 3, 6),
('2025-09-01', '2025-09-03', 'Confirmada', 1000.00, 7, 'Com banheira', 4, 7),
('2025-07-05', '2025-07-07', 'Cancelada', 300.00, 1, 'Problema de saúde', 5, 10),
('2025-08-15', '2025-08-18', 'Confirmada', 900.00, 2, 'Quarto para não fumantes', 6, 11),
('2025-09-20', '2025-09-22', 'Pendente', 750.00, 6, 'Próximo ao elevador', 7, 12),
('2025-07-28', '2025-07-30', 'Confirmada', 300.00, 5, 'Check-in tardio', 8, 1),
('2025-08-03', '2025-08-07', 'Confirmada', 1200.00, 3, 'Berço extra', 9, 4),
('2025-09-05', '2025-09-10', 'Pendente', 2000.00, 4, 'Andar alto', 10, 15);


INSERT INTO Servicos (nome_ser, descricao_ser, preco_ser) VALUES
('Café da Manhã', 'Buffet completo no restaurante do hotel.', 35.00),
('Lavanderia Express', 'Serviço de lavanderia com entrega em 24h.', 50.00),
('Serviço de Quarto', 'Atendimento 24h para refeições no quarto.', 20.00),
('Minibar', 'Consumo de itens do frigobar do quarto.', 10.00),
('Estacionamento', 'Vaga de estacionamento coberta.', 25.00),
('Spa & Massagem', 'Sessões de massagem e acesso às instalações do spa.', 120.00),
('Piscina Aquecida', 'Acesso à piscina aquecida do hotel.', 15.00),
('Academia', 'Acesso à academia com equipamentos modernos.', 0.00), 
('Transfer Aeroporto', 'Serviço de transporte de/para o aeroporto.', 80.00),
('Aluguel de Bicicletas', 'Aluguel de bicicletas para passeios na região.', 30.00);

INSERT INTO ServicosConsumidos (quantidade_sco, data_consumo_sco, valor_total_sco, id_reserva_fk, id_servico_fk) VALUES
(2, '2025-08-02 08:00:00', 70.00, 1, 1), 
(1, '2025-08-11 10:00:00', 50.00, 2, 2), 
(1, '2025-07-22 19:00:00', 20.00, 3, 3),
(3, '2025-09-02 15:00:00', 30.00, 4, 4), 
(1, '2025-08-16 12:00:00', 25.00, 6, 5),
(2, '2025-08-04 08:30:00', 70.00, 1, 1), 
(1, '2025-07-23 20:00:00', 20.00, 3, 3), 
(2, '2025-09-01 16:00:00', 20.00, 4, 4), 
(1, '2025-08-04 10:00:00', 50.00, 9, 2),
(1, '2025-09-06 14:00:00', 25.00, 10, 5);

INSERT INTO Funcionarios (nome_fun, cpf_fun, data_nascimento_fun, rg_fun, salario_fun, email_fun, telefone_fun) VALUES
('Marcos Silva', '111.222.333-00', '1980-05-10', '11.222.333-4', 3000.00, 'marcos.s@hotel.com', '91111-2222'),
('Patrícia Souza', '444.555.666-11', '1992-08-22', '44.555.666-7', 2800.00, 'patricia.s@hotel.com', '93333-4444'),
('Ricardo Lima', '777.888.999-22', '1975-01-15', '77.888.999-0', 4500.00, 'ricardo.l@hotel.com', '95555-6666'),
('Sofia Costa', '000.111.222-33', '1998-11-30', '00.111.222-3', 2500.00, 'sofia.c@hotel.com', '97777-8888'),
('Thiago Alves', '333.444.555-44', '1987-03-03', '33.444.555-6', 3200.00, 'thiago.a@hotel.com', '99999-0000'),
('Valéria Mendes', '666.777.888-55', '1990-09-09', '66.777.888-9', 2900.00, 'valeria.m@hotel.com', '92222-1111'),
('Wellington Rocha', '999.000.111-66', '1983-02-14', '99.000.111-2', 3500.00, 'wellington.r@hotel.com', '94444-3333'),
('Camila Fernandes', '123.456.789-01', '1995-06-20', '12.345.678-9', 2700.00, 'camila.f@hotel.com', '91234-9876'),
('Gustavo Oliveira', '987.654.321-09', '1982-10-01', '98.765.432-1', 3100.00, 'gustavo.o@hotel.com', '98765-4321'),
('Larissa Santos', '456.789.012-34', '1990-01-11', '45.678.901-2', 2600.00, 'larissa.s@hotel.com', '95678-1234');

INSERT INTO Caixa (valor_inicial_cai, data_abertura_cai, data_fechamento_cai, 
total_entradas_cai, total_saidas_cai, id_funcionario_fk) VALUES
(100.00, '2025-07-10 08:00:00', '2025-07-10 17:00:00', 500.00, 50.00, 1),
(150.00, '2025-07-11 08:00:00', '2025-07-11 17:00:00', 700.00, 75.00, 2),
(200.00, '2025-07-12 08:00:00', '2025-07-12 17:00:00', 600.00, 60.00, 1),
(120.00, '2025-07-13 08:00:00', NULL, 300.00, 20.00, 3), 
(100.00, '2025-07-14 08:00:00', '2025-07-14 17:00:00', 800.00, 100.00, 4),
(150.00, '2025-07-15 08:00:00', '2025-07-15 17:00:00', 900.00, 80.00, 5),
(200.00, '2025-07-16 08:00:00', NULL, 450.00, 30.00, 6), 
(100.00, '2025-07-17 08:00:00', '2025-07-17 17:00:00', 750.00, 90.00, 1),
(180.00, '2025-07-18 08:00:00', '2025-07-18 17:00:00', 620.00, 55.00, 7),
(130.00, '2025-07-19 08:00:00', NULL, 380.00, 25.00, 8);

INSERT INTO Produtos (nome_pro, descricao_pro, preco_pro) VALUES
('Água Mineral 500ml', 'Garrafa de água mineral sem gás.', 5.00),
('Refrigerante Lata', 'Lata de refrigerante variados.', 8.00),
('Chocolate Barra', 'Barra de chocolate ao leite.', 12.00),
('Salgadinho Pequeno', 'Pacote de salgadinho de 50g.', 7.00),
('Cerveja Long Neck', 'Cerveja nacional long neck.', 15.00),
('Amendoim Torrado', 'Pacote de amendoim torrado e salgado.', 10.00),
('Biscoito Doce', 'Pacote de biscoito doce sortido.', 9.00),
('Suco Caixa 1L', 'Suco de fruta em caixa de 1 litro.', 18.00),
('Barra de Cereal', 'Barra de cereal sabor frutas.', 6.00),
('Vinho Tinto Pequeno', 'Garrafa pequena de vinho tinto.', 45.00);

INSERT INTO Estoque (quantidade_est, data_validade_est, lote_est, id_prod_fk) VALUES
(50, '2026-12-31', 'AGUA001', 1),
(40, '2026-11-30', 'REFRI002', 2),
(30, '2026-10-15', 'CHOC003', 3),
(25, '2026-09-01', 'SALG004', 4),
(35, '2027-01-20', 'CERV005', 5),
(20, '2026-08-01', 'AMEN006', 6),
(45, '2026-07-25', 'BISC007', 7),
(15, '2027-03-10', 'SUCO008', 8),
(60, '2026-06-05', 'CEREAL009', 9),
(10, '2027-05-01', 'VINHO010', 10);

INSERT INTO TiposPagamento (nome_tpg, descricao_tpg) VALUES
('Cartão de Crédito', 'Pagamento via cartão de crédito (Visa, Mastercard, etc.)'),
('Dinheiro', 'Pagamento em espécie'),
('Pix', 'Pagamento instantâneo via Pix'),
('Cartão de Débito', 'Pagamento via cartão de débito'),
('Transferência Bancária', 'Pagamento via transferência eletrônica'),
('Boleto Bancário', 'Pagamento via boleto bancário'),
('Cheque', 'Pagamento via cheque (sujeito a aprovação)'),
('Voucher', 'Pagamento via voucher ou cupom'),
('Débito Automático', 'Pagamento programado para débito em conta'),
('Criptomoeda', 'Pagamento via criptomoedas (ex: Bitcoin, Ethereum)');

INSERT INTO Vendas (data_ven, valor_total_ven, id_tipo_pagamento_fk, id_reserva_fk, id_funcionario_fk) VALUES
('2025-07-10 10:30:00', 25.00, 2, NULL, 1),
('2025-07-11 14:00:00', 30.00, 1, 1, 2),
('2025-07-12 11:45:00', 15.00, 2, NULL, 3),
('2025-07-13 16:20:00', 40.00, 3, 3, 4),
('2025-07-14 09:00:00', 18.00, 4, NULL, 5),
('2025-07-15 13:10:00', 22.00, 2, 6, 6),
('2025-07-16 18:00:00', 55.00, 1, NULL, 7),
('2025-07-17 09:30:00', 10.00, 3, 8, 8),
('2025-07-18 11:00:00', 33.00, 2, NULL, 9),
('2025-07-19 15:00:00', 48.00, 1, 9, 10);

INSERT INTO Pagamentos (valor_pag, data_pag, id_tipo_pagamento_fk, status_pag, id_reserva_fk, id_caixa_fk, id_venda_fk) VALUES
(600.00, '2025-07-28 10:00:00', 1, 'Concluído', 1, 1, NULL),
(450.00, '2025-08-09 11:00:00', 3, 'Pendente', 2, 2, NULL),
(1500.00, '2025-07-19 15:00:00', 2, 'Concluído', 3, 3, NULL),
(1000.00, '2025-08-30 09:00:00', 4, 'Pendente', 4, 4, NULL),
(300.00, '2025-07-04 14:00:00', 2, 'Reembolsado', 5, 1, NULL),
(900.00, '2025-08-14 16:00:00', 1, 'Concluído', 6, 5, NULL),
(750.00, '2025-09-19 10:00:00', 3, 'Pendente', 7, 6, NULL),
(300.00, '2025-07-27 11:00:00', 2, 'Concluído', 8, 7, NULL),
(1200.00, '2025-08-02 13:00:00', 1, 'Concluído', 9, 8, NULL),
(2000.00, '2025-09-04 14:00:00', 3, 'Pendente', 10, 9, NULL),
(25.00, '2025-07-10 10:35:00', 2, 'Concluído', NULL, 1, 1),
(30.00, '2025-07-11 14:05:00', 1, 'Concluído', 1, 2, 2),
(15.00, '2025-07-12 11:50:00', 2, 'Concluído', NULL, 3, 3),
(40.00, '2025-07-13 16:25:00', 3, 'Concluído', 3, 4, 4),
(55.00, '2025-07-16 18:05:00', 1, 'Concluído', NULL, 6, 7);

INSERT INTO ItensVenda (quantidade_ive, preco_unitario_ive, id_venda_fk, id_prod_fk) VALUES
(2, 8.00, 1, 2),
(1, 9.00, 1, 7),
(1, 12.00, 2, 3),
(1, 18.00, 2, 8),
(3, 5.00, 3, 1),
(2, 15.00, 4, 5),
(1, 10.00, 4, 6),
(1, 6.00, 5, 9),
(1, 7.00, 6, 4),
(2, 5.00, 6, 1),
(1, 45.00, 7, 10),
(1, 10.00, 8, 6),
(2, 8.00, 9, 2),
(1, 15.00, 10, 5),
(1, 18.00, 10, 8);


############################# Consultas usando junção de tabelas (INNER JOIN, LEFT JOIN, RIGHT JOIN) ###################################################

# INNER JOIN -  Reservas confirmadas com dados do hóspede(nome) e do quarto(numero)
SELECT 
r.id_reserva,
h.nome_hos AS NomeHospede,
q.numero_qua AS nNumeroQuarto,
r.data_checkin_res,
r.data_checkout_res,
r.status_res,
r.valor_total_res, 
r.numero_hospedes_res,
r.observacoes_res
FROM Reservas r
INNER JOIN Hospedes h ON r.id_hospede_fk = h.id_hospede
INNER JOIN Quartos q ON r.id_quarto_fk = q.id_quarto
WHERE r.status_res = 'Confirmada';

# LEFT JOIN: Todas as reservas mesmo se nao houver serviços consumidos
SELECT
r.id_reserva,
h.nome_hos AS NomeHospede,
s.nome_ser AS Servico,
sc.quantidade_sco,
sc.valor_total_sco,
sc.data_consumo_sco
FROM Reservas r
LEFT JOIN Hospedes h ON r.id_hospede_fk = h.id_hospede
LEFT JOIN ServicosConsumidos sc ON r.id_reserva = sc.id_reserva_fk
LEFT JOIN Servicos s ON sc.id_servico_fk = s.id_servico;

# RIGHT JOIN: Produtos vendidos, mesmo que a venda não esteja associada a uma reserva
SELECT 
p.*,
iv.quantidade_ive,
iv.preco_unitario_ive,
v.id_reserva_fk,
v.data_ven
FROM ItensVenda iv
RIGHT JOIN Produtos p ON iv.id_prod_fk = p.id_prod
RIGHT JOIN Vendas v ON iv.id_venda_fk = v.id_venda;

############################################# Subconsultas (consultas aninhadas) #######################################################################
# Hóspedes com reservas confirmadas
SELECT * FROM Hospedes WHERE id_hospede IN (SELECT id_hospede_fk FROM Reservas WHERE status_res = 'Confirmada');

# Reservas que têm valor total maior que a média dos valores totais das reservas
SELECT * FROM Reservas WHERE valor_total_res > (SELECT AVG(valor_total_res) FROM Reservas);

############################################################# GROUP BY & HAVING #############################################################################
# Quantidade total de serviços consumidos por cada serviço
SELECT s.nome_ser, 
SUM(sc.quantidade_sco) AS TotalConsumido
FROM Servicos s
INNER JOIN ServicosConsumidos sc ON s.id_servico = sc.id_servico_fk
GROUP BY s.nome_ser;

# Hóspedes que consumiram mais de 50 Reais em serviços
SELECT 
r.id_hospede_fk,
h.nome_hos,
SUM(sc.valor_total_sco) AS TotalConsumido
FROM ServicosConsumidos sc
INNER JOIN Reservas r ON sc.id_reserva_fk = r.id_reserva
INNER JOIN Hospedes h ON r.id_hospede_fk = h.id_hospede
GROUP BY r.id_hospede_fk, h.nome_hos
HAVING SUM(sc.valor_total_sco) > 40;




