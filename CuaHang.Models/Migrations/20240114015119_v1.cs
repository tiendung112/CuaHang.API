using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CuaHang.Models.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name_product_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image_type_product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_typeID = table.Column<int>(type: "int", nullable: false),
                    name_product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    avartar_image_product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    discount = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    number_of_views = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_product_type_product_typeID",
                        column: x => x.product_typeID,
                        principalTable: "product_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiKhoan_Roles_roleId",
                        column: x => x.roleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDung_TaiKhoan_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "TaiKhoan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Refresh_token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    accountID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refresh_token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Refresh_token_TaiKhoan_accountID",
                        column: x => x.accountID,
                        principalTable: "TaiKhoan",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "XacNhanEmail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaiKhoanID = table.Column<int>(type: "int", nullable: false),
                    ThoiGianHetHan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaXacNhan = table.Column<int>(type: "int", nullable: true),
                    DaXacNhan = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XacNhanEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_XacNhanEmail_TaiKhoan_TaiKhoanID",
                        column: x => x.TaiKhoanID,
                        principalTable: "TaiKhoan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    nguoiDungId = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_NguoiDung_nguoiDungId",
                        column: x => x.nguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    paymentID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    nguoiDungId = table.Column<int>(type: "int", nullable: true),
                    original_price = table.Column<double>(type: "float", nullable: true),
                    actual_price = table.Column<double>(type: "float", nullable: true),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_statusID = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_NguoiDung_nguoiDungId",
                        column: x => x.nguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_order_order_status_order_statusID",
                        column: x => x.order_statusID,
                        principalTable: "order_status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_payment_paymentID",
                        column: x => x.paymentID,
                        principalTable: "payment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product_review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    nguoiDungId = table.Column<int>(type: "int", nullable: true),
                    content_rated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    point_evaluation = table.Column<int>(type: "int", nullable: false),
                    content_seen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_review_NguoiDung_nguoiDungId",
                        column: x => x.nguoiDungId,
                        principalTable: "NguoiDung",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_product_review_product_productID",
                        column: x => x.productID,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart_item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(type: "int", nullable: false),
                    cartID = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_item_Carts_cartID",
                        column: x => x.cartID,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_item_product_productId",
                        column: x => x.productId,
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderID = table.Column<int>(type: "int", nullable: false),
                    productID = table.Column<int>(type: "int", nullable: true),
                    price_total = table.Column<double>(type: "float", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_detail_order_orderID",
                        column: x => x.orderID,
                        principalTable: "order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_detail_product_productID",
                        column: x => x.productID,
                        principalTable: "product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_item_cartID",
                table: "Cart_item",
                column: "cartID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_item_productId",
                table: "Cart_item",
                column: "productId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_nguoiDungId",
                table: "Carts",
                column: "nguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_TaiKhoanId",
                table: "NguoiDung",
                column: "TaiKhoanId",
                unique: true,
                filter: "[TaiKhoanId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_order_nguoiDungId",
                table: "order",
                column: "nguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_order_order_statusID",
                table: "order",
                column: "order_statusID");

            migrationBuilder.CreateIndex(
                name: "IX_order_paymentID",
                table: "order",
                column: "paymentID");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_orderID",
                table: "order_detail",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_productID",
                table: "order_detail",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_product_product_typeID",
                table: "product",
                column: "product_typeID");

            migrationBuilder.CreateIndex(
                name: "IX_product_review_nguoiDungId",
                table: "product_review",
                column: "nguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_product_review_productID",
                table: "product_review",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_Refresh_token_accountID",
                table: "Refresh_token",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_TaiKhoan_roleId",
                table: "TaiKhoan",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "IX_XacNhanEmail_TaiKhoanID",
                table: "XacNhanEmail",
                column: "TaiKhoanID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart_item");

            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "product_review");

            migrationBuilder.DropTable(
                name: "Refresh_token");

            migrationBuilder.DropTable(
                name: "XacNhanEmail");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "order_status");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "product_type");

            migrationBuilder.DropTable(
                name: "TaiKhoan");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
