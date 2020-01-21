var cartModule = angular.module('virtoCommerce.cartModule');

cartModule.service('virtoCommerce.cartModule.countriesService', function () {
	this.countries = [
		{
			"name": "Canada",
			"code2": "CA",
			"code3": "CAN",
			"regions": [
				{
					"name": "Alberta",
					"code": "AB"
				},
				{
					"name": "British Columbia",
					"code": "BC"
				},
				{
					"name": "Manitoba",
					"code": "MB"
				},
				{
					"name": "New Brunswick",
					"code": "NB"
				},
				{
					"name": "Newfoundland",
					"code": "NL"
				},
				{
					"name": "Northwest Territories",
					"code": "NT"
				},
				{
					"name": "Nova Scotia",
					"code": "NS"
				},
				{
					"name": "Nunavut",
					"code": "NU"
				},
				{
					"name": "Ontario",
					"code": "ON"
				},
				{
					"name": "Prince Edward Island",
					"code": "PE"
				},
				{
					"name": "Quebec",
					"code": "QC"
				},
				{
					"name": "Saskatchewan",
					"code": "SK"
				},
				{
					"name": "Yukon",
					"code": "YT"
				}
			],
			"regionType": "Province"
		},
		{
			"name": "United States",
			"code2": "US",
			"code3": "USA",
			"regions": [
				{
					"name": "Alabama",
					"code": "AL"
				},
				{
					"name": "Alaska",
					"code": "AK"
				},
				{
					"name": "American Samoa",
					"code": "AS"
				},
				{
					"name": "Arizona",
					"code": "AZ"
				},
				{
					"name": "Arkansas",
					"code": "AR"
				},
				{
					"name": "California",
					"code": "CA"
				},
				{
					"name": "Colorado",
					"code": "CO"
				},
				{
					"name": "Connecticut",
					"code": "CT"
				},
				{
					"name": "Delaware",
					"code": "DE"
				},
				{
					"name": "Federated States of Micronesia",
					"code": "FM"
				},
				{
					"name": "Florida",
					"code": "FL"
				},
				{
					"name": "Georgia",
					"code": "GA"
				},
				{
					"name": "Guam",
					"code": "GU"
				},
				{
					"name": "Hawaii",
					"code": "HI"
				},
				{
					"name": "Idaho",
					"code": "ID"
				},
				{
					"name": "Illinois",
					"code": "IL"
				},
				{
					"name": "Indiana",
					"code": "IN"
				},
				{
					"name": "Iowa",
					"code": "IA"
				},
				{
					"name": "Kansas",
					"code": "KS"
				},
				{
					"name": "Kentucky",
					"code": "KY"
				},
				{
					"name": "Louisiana",
					"code": "LA"
				},
				{
					"name": "Maine",
					"code": "ME"
				},
				{
					"name": "Marshall Islands",
					"code": "MH"
				},
				{
					"name": "Maryland",
					"code": "MD"
				},
				{
					"name": "Massachusetts",
					"code": "MA"
				},
				{
					"name": "Michigan",
					"code": "MI"
				},
				{
					"name": "Minnesota",
					"code": "MN"
				},
				{
					"name": "Mississippi",
					"code": "MS"
				},
				{
					"name": "Missouri",
					"code": "MO"
				},
				{
					"name": "Montana",
					"code": "MT"
				},
				{
					"name": "Nebraska",
					"code": "NE"
				},
				{
					"name": "Nevada",
					"code": "NV"
				},
				{
					"name": "New Hampshire",
					"code": "NH"
				},
				{
					"name": "New Jersey",
					"code": "NJ"
				},
				{
					"name": "New Mexico",
					"code": "NM"
				},
				{
					"name": "New York",
					"code": "NY"
				},
				{
					"name": "North Carolina",
					"code": "NC"
				},
				{
					"name": "North Dakota",
					"code": "ND"
				},
				{
					"name": "Northern Mariana Islands",
					"code": "MP"
				},
				{
					"name": "Ohio",
					"code": "OH"
				},
				{
					"name": "Oklahoma",
					"code": "OK"
				},
				{
					"name": "Oregon",
					"code": "OR"
				},
				{
					"name": "Palau",
					"code": "PW"
				},
				{
					"name": "Pennsylvania",
					"code": "PA"
				},
				{
					"name": "Puerto Rico",
					"code": "PR"
				},
				{
					"name": "Rhode Island",
					"code": "RI"
				},
				{
					"name": "South Carolina",
					"code": "SC"
				},
				{
					"name": "South Dakota",
					"code": "SD"
				},
				{
					"name": "Tennessee",
					"code": "TN"
				},
				{
					"name": "Texas",
					"code": "TX"
				},
				{
					"name": "Utah",
					"code": "UT"
				},
				{
					"name": "Vermont",
					"code": "VT"
				},
				{
					"name": "Virginia",
					"code": "VA"
				},
				{
					"name": "Washington",
					"code": "WA"
				},
				{
					"name": "Washington DC",
					"code": "DC"
				},
				{
					"name": "West Virginia",
					"code": "WV"
				},
				{
					"name": "Wisconsin",
					"code": "WI"
				},
				{
					"name": "Wyoming",
					"code": "WY"
				},
				{
					"name": "Virgin Islands",
					"code": "VI"
				},
				{
					"name": "Armed Forces Americas",
					"code": "AA"
				},
				{
					"name": "Armed Forces Europe",
					"code": "AE"
				},
				{
					"name": "Armed Forces Pacific",
					"code": "AP"
				}
			],
			"regionType": "State"
		},
		{
			"name": "United Kingdom",
			"code2": "GB",
			"code3": "GBR",
			"regionType": "Region"
		},
		{
			"name": "Albania",
			"code2": "AL",
			"code3": "ALB",
			"regionType": "Region"
		},
		{
			"name": "Algeria",
			"code2": "DZ",
			"code3": "DZA",
			"regionType": "Province"
		},
		{
			"name": "Andorra",
			"code2": "AD",
			"code3": "AND",
			"regionType": "Region"
		},
		{
			"name": "Angola",
			"code2": "AO",
			"code3": "AGO",
			"regionType": "Region"
		},
		{
			"name": "Argentina",
			"code2": "AR",
			"code3": "ARG",
			"regions": [
				{
					"name": "Buenos Aires City",
					"code": "C"
				},
				{
					"name": "Buenos Aires",
					"code": "B"
				},
				{
					"name": "Catamarca",
					"code": "K"
				},
				{
					"name": "Chaco",
					"code": "H"
				},
				{
					"name": "Chobut",
					"code": "U"
				},
				{
					"name": "Córdoba",
					"code": "X"
				},
				{
					"name": "Corrientes",
					"code": "W"
				},
				{
					"name": "Ente Ríos",
					"code": "E"
				},
				{
					"name": "Formosa",
					"code": "P"
				},
				{
					"name": "Jujuy",
					"code": "Y"
				},
				{
					"name": "La Pampa",
					"code": "L"
				},
				{
					"name": "La Rioja",
					"code": "F"
				},
				{
					"name": "Mendoza",
					"code": "M"
				},
				{
					"name": "Misiones",
					"code": "N"
				},
				{
					"name": "Neuquén",
					"code": "Q"
				},
				{
					"name": "Río Negro",
					"code": "R"
				},
				{
					"name": "Salta",
					"code": "A"
				},
				{
					"name": "San Juan",
					"code": "J"
				},
				{
					"name": "San Luis",
					"code": "D"
				},
				{
					"name": "Santa Cruz",
					"code": "Z"
				},
				{
					"name": "Santa Fe",
					"code": "S"
				},
				{
					"name": "Santiago Del Estero",
					"code": "G"
				},
				{
					"name": "Tierra del Fuego",
					"code": "V"
				},
				{
					"name": "Tucumán",
					"code": "T"
				}
			],
			"regionType": "Province"
		},
		{
			"name": "Armenia",
			"code2": "AM",
			"code3": "ARM",
			"regionType": "Region"
		},
		{
			"name": "Aruba",
			"code2": "AW",
			"code3": "ABW",
			"regionType": "Region"
		},
		{
			"name": "Antigua And Barbuda",
			"code2": "AG",
			"code3": "ATG",
			"regionType": "Region"
		},
		{
			"name": "Australia",
			"code2": "AU",
			"code3": "AUS",
			"regions": [
				{
					"name": "Australian Capital Territory",
					"code": "ACT"
				},
				{
					"name": "New South Wales",
					"code": "NSW"
				},
				{
					"name": "Northern Territory",
					"code": "NT"
				},
				{
					"name": "Queensland",
					"code": "QLD"
				},
				{
					"name": "South Australia",
					"code": "SA"
				},
				{
					"name": "Tasmania",
					"code": "TAS"
				},
				{
					"name": "Victoria",
					"code": "VIC"
				},
				{
					"name": "Western Australia",
					"code": "WA"
				}
			],
			"regionType": "State/Territory"
		},
		{
			"name": "Austria",
			"code2": "AT",
			"code3": "AUT",
			"regionType": "Region"
		},
		{
			"name": "Azerbaijan",
			"code2": "AZ",
			"code3": "AZE",
			"regionType": "Region"
		},
		{
			"name": "Bangladesh",
			"code2": "BD",
			"code3": "BGD",
			"regionType": "Region"
		},
		{
			"name": "Bahamas",
			"code2": "BS",
			"code3": "BHS",
			"regionType": "Region"
		},
		{
			"name": "Bahrain",
			"code2": "BH",
			"code3": "BHR",
			"regionType": "Region"
		},
		{
			"name": "Barbados",
			"code2": "BB",
			"code3": "BRB",
			"regionType": "Region"
		},
		{
			"name": "Belarus",
			"code2": "BY",
			"code3": "BLR",
			"regionType": "Province"
		},
		{
			"name": "Belgium",
			"code2": "BE",
			"code3": "BEL",
			"regionType": "Region"
		},
		{
			"name": "Belize",
			"code2": "BZ",
			"code3": "BLZ",
			"regionType": "Region"
		},
		{
			"name": "Bermuda",
			"code2": "BM",
			"code3": "BMU",
			"regionType": "Region"
		},
		{
			"name": "Bhutan",
			"code2": "BT",
			"code3": "BTN",
			"regionType": "Region"
		},
		{
			"name": "Bolivia",
			"code2": "BO",
			"code3": "BOL",
			"regionType": "Region"
		},
		{
			"name": "Bosnia And Herzegovina",
			"code2": "BA",
			"code3": "BIH",
			"regionType": "Region"
		},
		{
			"name": "Botswana",
			"code2": "BW",
			"code3": "BWA",
			"regionType": "Region"
		},
		{
			"name": "Brazil",
			"code2": "BR",
			"code3": "BRA",
			"regions": [
				{
					"name": "Acre",
					"code": "AC"
				},
				{
					"name": "Alagoas",
					"code": "AL"
				},
				{
					"name": "Amapá",
					"code": "AP"
				},
				{
					"name": "Amazonas",
					"code": "AM"
				},
				{
					"name": "Bahia",
					"code": "BA"
				},
				{
					"name": "Ceará",
					"code": "CE"
				},
				{
					"name": "Distrito Federal",
					"code": "DF"
				},
				{
					"name": "Espírito Santo",
					"code": "ES"
				},
				{
					"name": "Goiás",
					"code": "GO"
				},
				{
					"name": "Maranhão",
					"code": "MA"
				},
				{
					"name": "Mato Grosso",
					"code": "MT"
				},
				{
					"name": "Mato Grosso do Sul",
					"code": "MS"
				},
				{
					"name": "Minas Gerais",
					"code": "MG"
				},
				{
					"name": "Pará",
					"code": "PA"
				},
				{
					"name": "Paraíba",
					"code": "PB"
				},
				{
					"name": "Paraná",
					"code": "PR"
				},
				{
					"name": "Pernambuco",
					"code": "PE"
				},
				{
					"name": "Piauí",
					"code": "PI"
				},
				{
					"name": "Rio de Janeiro",
					"code": "RJ"
				},
				{
					"name": "Rio Grande do Norte",
					"code": "RN"
				},
				{
					"name": "Rio Grande do Sul",
					"code": "RS"
				},
				{
					"name": "Rondônia",
					"code": "RO"
				},
				{
					"name": "Roraima",
					"code": "RR"
				},
				{
					"name": "Santa Catarina",
					"code": "SC"
				},
				{
					"name": "São Paulo",
					"code": "SP"
				},
				{
					"name": "Sergipe",
					"code": "SE"
				},
				{
					"name": "Tocantins",
					"code": "TO"
				}
			],
			"regionType": "State"
		},
		{
			"name": "Brunei",
			"code2": "BN",
			"code3": "BRN",
			"regionType": "Region"
		},
		{
			"name": "Bulgaria",
			"code2": "BG",
			"code3": "BGR",
			"regionType": "Region"
		},
		{
			"name": "Cambodia",
			"code2": "KH",
			"code3": "KHM",
			"regionType": "Region"
		},
		{
			"name": "Republic of Cameroon",
			"code2": "CM",
			"code3": "CMR",
			"regionType": "Region"
		},
		{
			"name": "Cayman Islands",
			"code2": "KY",
			"code3": "CYM",
			"regionType": "Region"
		},
		{
			"name": "Chile",
			"code2": "CL",
			"code3": "CHL",
			"regionType": "State"
		},
		{
			"name": "China",
			"code2": "CN",
			"code3": "CHN",
			"regionType": "Region"
		},
		{
			"name": "Colombia",
			"code2": "CO",
			"code3": "COL",
			"regionType": "Region"
		},
		{
			"name": "Comoros",
			"code2": "KM",
			"code3": "COM",
			"regionType": "Region"
		},
		{
			"name": "Congo",
			"code2": "CG",
			"code3": "COG",
			"regionType": "Region"
		},
		{
			"name": "Costa Rica",
			"code2": "CR",
			"code3": "CRI",
			"regionType": "Region"
		},
		{
			"name": "Côte d'Ivoire",
			"code2": "CI",
			"code3": "CIV",
			"regionType": "Region"
		},
		{
			"name": "Croatia",
			"code2": "HR",
			"code3": "HRV",
			"regionType": "Region"
		},
		{
			"name": "Curaçao",
			"code2": "CW",
			"code3": "CUW",
			"regionType": "Region"
		},
		{
			"name": "Cyprus",
			"code2": "CY",
			"code3": "CYP",
			"regionType": "Region"
		},
		{
			"name": "Czech Republic",
			"code2": "CZ",
			"code3": "CZE",
			"regionType": "Region"
		},
		{
			"name": "Denmark",
			"code2": "DK",
			"code3": "DNK",
			"regionType": "Region"
		},
		{
			"name": "Dominica",
			"code2": "DM",
			"code3": "DMA",
			"regionType": "Region"
		},
		{
			"name": "Dominican Republic",
			"code2": "DO",
			"code3": "DOM",
			"regionType": "Region"
		},
		{
			"name": "Ecuador",
			"code2": "EC",
			"code3": "ECU",
			"regionType": "Region"
		},
		{
			"name": "Egypt",
			"code2": "EG",
			"code3": "EGY",
			"regions": [
				{
					"name": "Dakahlia",
					"code": "DK"
				},
				{
					"name": "Red Sea",
					"code": "BA"
				},
				{
					"name": "Beheira",
					"code": "BH"
				},
				{
					"name": "Faiyum",
					"code": "FYM"
				},
				{
					"name": "Gharbia",
					"code": "GH"
				},
				{
					"name": "Alexandria",
					"code": "ALX"
				},
				{
					"name": "Ismailia",
					"code": "IS"
				},
				{
					"name": "Giza",
					"code": "GZ"
				},
				{
					"name": "Monufia",
					"code": "MNF"
				},
				{
					"name": "Minya",
					"code": "MN"
				},
				{
					"name": "Cairo",
					"code": "C"
				},
				{
					"name": "Qalyubia",
					"code": "KB"
				},
				{
					"name": "Luxor",
					"code": "LX"
				},
				{
					"name": "New Valley",
					"code": "WAD"
				},
				{
					"name": "Al Sharqia",
					"code": "SHR"
				},
				{
					"name": "6th of October",
					"code": "SU"
				},
				{
					"name": "Suez",
					"code": "SUZ"
				},
				{
					"name": "Aswan",
					"code": "ASN"
				},
				{
					"name": "Asyut",
					"code": "AST"
				},
				{
					"name": "Beni Suef",
					"code": "BNS"
				},
				{
					"name": "Port Said",
					"code": "PTS"
				},
				{
					"name": "Damietta",
					"code": "DT"
				},
				{
					"name": "Helwan",
					"code": "HU"
				},
				{
					"name": "South Sinai",
					"code": "JS"
				},
				{
					"name": "Kafr el-Sheikh",
					"code": "KFS"
				},
				{
					"name": "Matrouh",
					"code": "MT"
				},
				{
					"name": "Qena",
					"code": "KN"
				},
				{
					"name": "North Sinai",
					"code": "SIN"
				},
				{
					"name": "Sohag",
					"code": "SHG"
				}
			],
			"regionType": "Governorate"
		},
		{
			"name": "El Salvador",
			"code2": "SV",
			"code3": "SLV",
			"regionType": "Region"
		},
		{
			"name": "Estonia",
			"code2": "EE",
			"code3": "EST",
			"regionType": "Region"
		},
		{
			"name": "Ethiopia",
			"code2": "ET",
			"code3": "ETH",
			"regionType": "Region"
		},
		{
			"name": "Faroe Islands",
			"code2": "FO",
			"code3": "FRO",
			"regionType": "Region"
		},
		{
			"name": "Fiji",
			"code2": "FJ",
			"code3": "FJI",
			"regionType": "Region"
		},
		{
			"name": "Finland",
			"code2": "FI",
			"code3": "FIN",
			"regionType": "Region"
		},
		{
			"name": "France",
			"code2": "FR",
			"code3": "FRA",
			"regionType": "Region"
		},
		{
			"name": "French Polynesia",
			"code2": "PF",
			"code3": "PYF",
			"regionType": "Region"
		},
		{
			"name": "Gambia",
			"code2": "GM",
			"code3": "GMB",
			"regionType": "Region"
		},
		{
			"name": "Georgia",
			"code2": "GE",
			"code3": "GEO",
			"regionType": "Region"
		},
		{
			"name": "Germany",
			"code2": "DE",
			"code3": "DEU",
			"regionType": "Region"
		},
		{
			"name": "Ghana",
			"code2": "GH",
			"code3": "GHA",
			"regionType": "Region"
		},
		{
			"name": "Gibraltar",
			"code2": "GI",
			"code3": "GIB",
			"regionType": "Region"
		},
		{
			"name": "Greece",
			"code2": "GR",
			"code3": "GRC",
			"regionType": "Region"
		},
		{
			"name": "Greenland",
			"code2": "GL",
			"code3": "GRL",
			"regionType": "Region"
		},
		{
			"name": "Grenada",
			"code2": "GD",
			"code3": "GRD",
			"regionType": "Region"
		},
		{
			"name": "Guadeloupe",
			"code2": "GP",
			"code3": "GLP",
			"regionType": "Region"
		},
		{
			"name": "Guatemala",
			"code2": "GT",
			"code3": "GTM",
			"regions": [
				{
					"name": "Alta Verapaz",
					"code": "AVE"
				},
				{
					"name": "Baja Verapaz",
					"code": "BVE"
				},
				{
					"name": "Chimaltenango",
					"code": "CMT"
				},
				{
					"name": "Chiquimula",
					"code": "CQM"
				},
				{
					"name": "El Progreso",
					"code": "EPR"
				},
				{
					"name": "Escuintla",
					"code": "ESC"
				},
				{
					"name": "Guatemala",
					"code": "GUA"
				},
				{
					"name": "Huehuetenango",
					"code": "HUE"
				},
				{
					"name": "Izabal",
					"code": "IZA"
				},
				{
					"name": "Jalapa",
					"code": "JAL"
				},
				{
					"name": "Jutiapa",
					"code": "JUT"
				},
				{
					"name": "Petén",
					"code": "PET"
				},
				{
					"name": "Quetzaltenango",
					"code": "QUE"
				},
				{
					"name": "Quiché",
					"code": "QUI"
				},
				{
					"name": "Retalhuleu",
					"code": "RET"
				},
				{
					"name": "Sacatepéquez",
					"code": "SAC"
				},
				{
					"name": "San Marcos",
					"code": "SMA"
				},
				{
					"name": "Santa Rosa",
					"code": "SRO"
				},
				{
					"name": "Sololá",
					"code": "SOL"
				},
				{
					"name": "Suchitepéquez",
					"code": "SUC"
				},
				{
					"name": "Totonicapán",
					"code": "TOT"
				},
				{
					"name": "Zacapa",
					"code": "ZAC"
				}
			],
			"regionType": "Region"
		},
		{
			"name": "Guernsey",
			"code2": "GG",
			"code3": "GGY",
			"regionType": "Region"
		},
		{
			"name": "Guyana",
			"code2": "GY",
			"code3": "GUY",
			"regionType": "Region"
		},
		{
			"name": "Haiti",
			"code2": "HT",
			"code3": "HTI",
			"regionType": "Region"
		},
		{
			"name": "Honduras",
			"code2": "HN",
			"code3": "HND",
			"regionType": "Region"
		},
		{
			"name": "Hong Kong",
			"code2": "HK",
			"code3": "HKG",
			"regionType": "Region"
		},
		{
			"name": "Hungary",
			"code2": "HU",
			"code3": "HUN",
			"regionType": "Region"
		},
		{
			"name": "Iceland",
			"code2": "IS",
			"code3": "ISL",
			"regionType": "Region"
		},
		{
			"name": "India",
			"code2": "IN",
			"code3": "IND",
			"regions": [
				{
					"name": "Andaman and Nicobar",
					"code": "AN"
				},
				{
					"name": "Andhra Pradesh",
					"code": "AP"
				},
				{
					"name": "Arunachal Pradesh",
					"code": "AR"
				},
				{
					"name": "Assam",
					"code": "AS"
				},
				{
					"name": "Bihar",
					"code": "BR"
				},
				{
					"name": "Chandigarh",
					"code": "CH"
				},
				{
					"name": "Chattisgarh",
					"code": "CG"
				},
				{
					"name": "Dadra and Nagar Haveli",
					"code": "DN"
				},
				{
					"name": "Daman and Diu",
					"code": "DD"
				},
				{
					"name": "Delhi",
					"code": "DL"
				},
				{
					"name": "Goa",
					"code": "GA"
				},
				{
					"name": "Gujarat",
					"code": "GJ"
				},
				{
					"name": "Haryana",
					"code": "HR"
				},
				{
					"name": "Himachal Pradesh",
					"code": "HP"
				},
				{
					"name": "Jammu and Kashmir",
					"code": "JK"
				},
				{
					"name": "Jharkhand",
					"code": "JH"
				},
				{
					"name": "Karnataka",
					"code": "KA"
				},
				{
					"name": "Kerala",
					"code": "KL"
				},
				{
					"name": "Lakshadweep",
					"code": "LD"
				},
				{
					"name": "Madhya Pradesh",
					"code": "MP"
				},
				{
					"name": "Maharashtra",
					"code": "MH"
				},
				{
					"name": "Manipur",
					"code": "MN"
				},
				{
					"name": "Meghalaya",
					"code": "ML"
				},
				{
					"name": "Mizoram",
					"code": "MZ"
				},
				{
					"name": "Nagaland",
					"code": "NL"
				},
				{
					"name": "Orissa",
					"code": "OR"
				},
				{
					"name": "Puducherry",
					"code": "PY"
				},
				{
					"name": "Punjab",
					"code": "PB"
				},
				{
					"name": "Rajasthan",
					"code": "RJ"
				},
				{
					"name": "Sikkim",
					"code": "SK"
				},
				{
					"name": "Tamil Nadu",
					"code": "TN"
				},
				{
					"name": "Telangana",
					"code": "TS"
				},
				{
					"name": "Tripura",
					"code": "TR"
				},
				{
					"name": "Uttar Pradesh",
					"code": "UP"
				},
				{
					"name": "Uttarakhand",
					"code": "UK"
				},
				{
					"name": "West Bengal",
					"code": "WB"
				}
			],
			"regionType": "State"
		},
		{
			"name": "Indonesia",
			"code2": "ID",
			"code3": "IDN",
			"regions": [
				{
					"name": "Aceh",
					"code": "AC"
				},
				{
					"name": "Bali",
					"code": "BA"
				},
				{
					"name": "Bangka Belitung",
					"code": "BB"
				},
				{
					"name": "Banten",
					"code": "BT"
				},
				{
					"name": "Bengkulu",
					"code": "BE"
				},
				{
					"name": "Gorontalo",
					"code": "GO"
				},
				{
					"name": "Jakarta",
					"code": "JK"
				},
				{
					"name": "Jambi",
					"code": "JA"
				},
				{
					"name": "Jawa Barat",
					"code": "JB"
				},
				{
					"name": "Jawa Tengah",
					"code": "JT"
				},
				{
					"name": "Jawa Timur",
					"code": "JI"
				},
				{
					"name": "Kalimantan Barat",
					"code": "KB"
				},
				{
					"name": "Kalimantan Selatan",
					"code": "KS"
				},
				{
					"name": "Kalimantan Tengah",
					"code": "KT"
				},
				{
					"name": "Kalimantan Timur",
					"code": "KI"
				},
				{
					"name": "Kalimantan Utara",
					"code": "KU"
				},
				{
					"name": "Kepulauan Riau",
					"code": "KR"
				},
				{
					"name": "Lampung",
					"code": "LA"
				},
				{
					"name": "Maluku",
					"code": "MA"
				},
				{
					"name": "Maluku Utara",
					"code": "MU"
				},
				{
					"name": "Nusa Tenggara Barat",
					"code": "NB"
				},
				{
					"name": "Nusa Tenggara Timur",
					"code": "NT"
				},
				{
					"name": "Papua",
					"code": "PA"
				},
				{
					"name": "Papua Barat",
					"code": "PB"
				},
				{
					"name": "Riau",
					"code": "RI"
				},
				{
					"name": "Sulawesi Barat",
					"code": "SR"
				},
				{
					"name": "Sulawesi Selatan",
					"code": "SN"
				},
				{
					"name": "Sulawesi Tengah",
					"code": "ST"
				},
				{
					"name": "Sulawesi Tenggara",
					"code": "SG"
				},
				{
					"name": "Sulawesi Utara",
					"code": "SA"
				},
				{
					"name": "Sumatra Barat",
					"code": "SB"
				},
				{
					"name": "Sumatra Selatan",
					"code": "SS"
				},
				{
					"name": "Sumatra Utara",
					"code": "SU"
				},
				{
					"name": "Yogyakarta",
					"code": "YO"
				}
			],
			"regionType": "Province"
		},
		{
			"name": "Ireland",
			"code2": "IE",
			"code3": "IRL",
			"regionType": "Region"
		},
		{
			"name": "Isle Of Man",
			"code2": "IM",
			"code3": "IMN",
			"regionType": "Region"
		},
		{
			"name": "Israel",
			"code2": "IL",
			"code3": "ISR",
			"regionType": "Region"
		},
		{
			"name": "Italy",
			"code2": "IT",
			"code3": "ITA",
			"regions": [
				{
					"name": "Agrigento",
					"code": "AG"
				},
				{
					"name": "Alessandria",
					"code": "AL"
				},
				{
					"name": "Ancona",
					"code": "AN"
				},
				{
					"name": "Aosta",
					"code": "AO"
				},
				{
					"name": "Arezzo",
					"code": "AR"
				},
				{
					"name": "Ascoli Piceno",
					"code": "AP"
				},
				{
					"name": "Asti",
					"code": "AT"
				},
				{
					"name": "Avellino",
					"code": "AV"
				},
				{
					"name": "Bari",
					"code": "BA"
				},
				{
					"name": "Barletta-Andria-Trani",
					"code": "BT"
				},
				{
					"name": "Belluno",
					"code": "BL"
				},
				{
					"name": "Benevento",
					"code": "BN"
				},
				{
					"name": "Bergamo",
					"code": "BG"
				},
				{
					"name": "Biella",
					"code": "BI"
				},
				{
					"name": "Bologna",
					"code": "BO"
				},
				{
					"name": "Bolzano",
					"code": "BZ"
				},
				{
					"name": "Brescia",
					"code": "BS"
				},
				{
					"name": "Brindisi",
					"code": "BR"
				},
				{
					"name": "Cagliari",
					"code": "CA"
				},
				{
					"name": "Caltanissetta",
					"code": "CL"
				},
				{
					"name": "Campobasso",
					"code": "CB"
				},
				{
					"name": "Carbonia-Iglesias",
					"code": "CI"
				},
				{
					"name": "Caserta",
					"code": "CE"
				},
				{
					"name": "Catania",
					"code": "CT"
				},
				{
					"name": "Catanzaro",
					"code": "CZ"
				},
				{
					"name": "Chieti",
					"code": "CH"
				},
				{
					"name": "Como",
					"code": "CO"
				},
				{
					"name": "Cosenza",
					"code": "CS"
				},
				{
					"name": "Cremona",
					"code": "CR"
				},
				{
					"name": "Crotone",
					"code": "KR"
				},
				{
					"name": "Cuneo",
					"code": "CN"
				},
				{
					"name": "Enna",
					"code": "EN"
				},
				{
					"name": "Fermo",
					"code": "FM"
				},
				{
					"name": "Ferrara",
					"code": "FE"
				},
				{
					"name": "Firenze",
					"code": "FI"
				},
				{
					"name": "Foggia",
					"code": "FG"
				},
				{
					"name": "Forlì-Cesena",
					"code": "FC"
				},
				{
					"name": "Frosinone",
					"code": "FR"
				},
				{
					"name": "Genova",
					"code": "GE"
				},
				{
					"name": "Gorizia",
					"code": "GO"
				},
				{
					"name": "Grosseto",
					"code": "GR"
				},
				{
					"name": "Imperia",
					"code": "IM"
				},
				{
					"name": "Isernia",
					"code": "IS"
				},
				{
					"name": "La Spezia",
					"code": "SP"
				},
				{
					"name": "L'Aquila",
					"code": "AQ"
				},
				{
					"name": "Latina",
					"code": "LT"
				},
				{
					"name": "Lecce",
					"code": "LE"
				},
				{
					"name": "Lecco",
					"code": "LC"
				},
				{
					"name": "Livorno",
					"code": "LI"
				},
				{
					"name": "Lodi",
					"code": "LO"
				},
				{
					"name": "Lucca",
					"code": "LU"
				},
				{
					"name": "Macerata",
					"code": "MC"
				},
				{
					"name": "Mantova",
					"code": "MN"
				},
				{
					"name": "Massa-Carrara",
					"code": "MS"
				},
				{
					"name": "Matera",
					"code": "MT"
				},
				{
					"name": "Medio Campidano",
					"code": "VS"
				},
				{
					"name": "Messina",
					"code": "ME"
				},
				{
					"name": "Milano",
					"code": "MI"
				},
				{
					"name": "Modena",
					"code": "MO"
				},
				{
					"name": "Monza e Brianza",
					"code": "MB"
				},
				{
					"name": "Napoli",
					"code": "NA"
				},
				{
					"name": "Novara",
					"code": "NO"
				},
				{
					"name": "Nuoro",
					"code": "NU"
				},
				{
					"name": "Ogliastra",
					"code": "OG"
				},
				{
					"name": "Olbia-Tempio",
					"code": "OT"
				},
				{
					"name": "Oristano",
					"code": "OR"
				},
				{
					"name": "Padova",
					"code": "PD"
				},
				{
					"name": "Palermo",
					"code": "PA"
				},
				{
					"name": "Parma",
					"code": "PR"
				},
				{
					"name": "Pavia",
					"code": "PV"
				},
				{
					"name": "Perugia",
					"code": "PG"
				},
				{
					"name": "Pesaro e Urbino",
					"code": "PU"
				},
				{
					"name": "Pescara",
					"code": "PE"
				},
				{
					"name": "Piacenza",
					"code": "PC"
				},
				{
					"name": "Pisa",
					"code": "PI"
				},
				{
					"name": "Pistoia",
					"code": "PT"
				},
				{
					"name": "Pordenone",
					"code": "PN"
				},
				{
					"name": "Potenza",
					"code": "PZ"
				},
				{
					"name": "Prato",
					"code": "PO"
				},
				{
					"name": "Ragusa",
					"code": "RG"
				},
				{
					"name": "Ravenna",
					"code": "RA"
				},
				{
					"name": "Reggio Calabria",
					"code": "RC"
				},
				{
					"name": "Reggio Emilia",
					"code": "RE"
				},
				{
					"name": "Rieti",
					"code": "RI"
				},
				{
					"name": "Rimini",
					"code": "RN"
				},
				{
					"name": "Roma",
					"code": "RM"
				},
				{
					"name": "Rovigo",
					"code": "RO"
				},
				{
					"name": "Salerno",
					"code": "SA"
				},
				{
					"name": "Sassari",
					"code": "SS"
				},
				{
					"name": "Savona",
					"code": "SV"
				},
				{
					"name": "Siena",
					"code": "SI"
				},
				{
					"name": "Siracusa",
					"code": "SR"
				},
				{
					"name": "Sondrio",
					"code": "SO"
				},
				{
					"name": "Taranto",
					"code": "TA"
				},
				{
					"name": "Teramo",
					"code": "TE"
				},
				{
					"name": "Terni",
					"code": "TR"
				},
				{
					"name": "Torino",
					"code": "TO"
				},
				{
					"name": "Trapani",
					"code": "TP"
				},
				{
					"name": "Trento",
					"code": "TN"
				},
				{
					"name": "Treviso",
					"code": "TV"
				},
				{
					"name": "Trieste",
					"code": "TS"
				},
				{
					"name": "Udine",
					"code": "UD"
				},
				{
					"name": "Varese",
					"code": "VA"
				},
				{
					"name": "Venezia",
					"code": "VE"
				},
				{
					"name": "Verbano-Cusio-Ossola",
					"code": "VB"
				},
				{
					"name": "Vercelli",
					"code": "VC"
				},
				{
					"name": "Verona",
					"code": "VR"
				},
				{
					"name": "Vibo Valentia",
					"code": "VV"
				},
				{
					"name": "Vicenza",
					"code": "VI"
				},
				{
					"name": "Viterbo",
					"code": "VT"
				}
			],
			"regionType": "Province"
		},
		{
			"name": "Jamaica",
			"code2": "JM",
			"code3": "JAM",
			"regionType": "Region"
		},
		{
			"name": "Japan",
			"code2": "JP",
			"code3": "JPN",
			"regions": [
				{
					"name": "Aichi",
					"code": "JP-23"
				},
				{
					"name": "Akita",
					"code": "JP-05"
				},
				{
					"name": "Aomori",
					"code": "JP-02"
				},
				{
					"name": "Chiba",
					"code": "JP-12"
				},
				{
					"name": "Ehime",
					"code": "JP-38"
				},
				{
					"name": "Fukui",
					"code": "JP-18"
				},
				{
					"name": "Fukuoka",
					"code": "JP-40"
				},
				{
					"name": "Fukushima",
					"code": "JP-07"
				},
				{
					"name": "Gifu",
					"code": "JP-21"
				},
				{
					"name": "Gunma",
					"code": "JP-10"
				},
				{
					"name": "Hiroshima",
					"code": "JP-34"
				},
				{
					"name": "Hokkaidō",
					"code": "JP-01"
				},
				{
					"name": "Hyōgo",
					"code": "JP-28"
				},
				{
					"name": "Ibaraki",
					"code": "JP-08"
				},
				{
					"name": "Ishikawa",
					"code": "JP-17"
				},
				{
					"name": "Iwate",
					"code": "JP-03"
				},
				{
					"name": "Kagawa",
					"code": "JP-37"
				},
				{
					"name": "Kagoshima",
					"code": "JP-46"
				},
				{
					"name": "Kanagawa",
					"code": "JP-14"
				},
				{
					"name": "Kōchi",
					"code": "JP-39"
				},
				{
					"name": "Kumamoto",
					"code": "JP-43"
				},
				{
					"name": "Kyōto",
					"code": "JP-26"
				},
				{
					"name": "Mie",
					"code": "JP-24"
				},
				{
					"name": "Miyagi",
					"code": "JP-04"
				},
				{
					"name": "Miyazaki",
					"code": "JP-45"
				},
				{
					"name": "Nagano",
					"code": "JP-20"
				},
				{
					"name": "Nagasaki",
					"code": "JP-42"
				},
				{
					"name": "Nara",
					"code": "JP-29"
				},
				{
					"name": "Niigata",
					"code": "JP-15"
				},
				{
					"name": "Ōita",
					"code": "JP-44"
				},
				{
					"name": "Okayama",
					"code": "JP-33"
				},
				{
					"name": "Okinawa",
					"code": "JP-47"
				},
				{
					"name": "Ōsaka",
					"code": "JP-27"
				},
				{
					"name": "Saga",
					"code": "JP-41"
				},
				{
					"name": "Saitama",
					"code": "JP-11"
				},
				{
					"name": "Shiga",
					"code": "JP-25"
				},
				{
					"name": "Shimane",
					"code": "JP-32"
				},
				{
					"name": "Shizuoka",
					"code": "JP-22"
				},
				{
					"name": "Tochigi",
					"code": "JP-09"
				},
				{
					"name": "Tokushima",
					"code": "JP-36"
				},
				{
					"name": "Tottori",
					"code": "JP-31"
				},
				{
					"name": "Toyama",
					"code": "JP-16"
				},
				{
					"name": "Tōkyō",
					"code": "JP-13"
				},
				{
					"name": "Wakayama",
					"code": "JP-30"
				},
				{
					"name": "Yamagata",
					"code": "JP-06"
				},
				{
					"name": "Yamaguchi",
					"code": "JP-35"
				},
				{
					"name": "Yamanashi",
					"code": "JP-19"
				}
			],
			"regionType": "Prefecture"
		},
		{
			"name": "Jersey",
			"code2": "JE",
			"code3": "JEY",
			"regionType": "Region"
		},
		{
			"name": "Jordan",
			"code2": "JO",
			"code3": "JOR",
			"regionType": "Region"
		},
		{
			"name": "Kazakhstan",
			"code2": "KZ",
			"code3": "KAZ",
			"regionType": "Region"
		},
		{
			"name": "Kenya",
			"code2": "KE",
			"code3": "KEN",
			"regionType": "Region"
		},
		{
			"name": "Kosovo",
			"code2": "XK",
			"code3": "XKX",
			"regionType": "Region"
		},
		{
			"name": "Kuwait",
			"code2": "KW",
			"code3": "KWT",
			"regionType": "Region"
		},
		{
			"name": "Kyrgyzstan",
			"code2": "KG",
			"code3": "KGZ",
			"regionType": "Region"
		},
		{
			"name": "Latvia",
			"code2": "LV",
			"code3": "LVA",
			"regionType": "Region"
		},
		{
			"name": "Lebanon",
			"code2": "LB",
			"code3": "LBN",
			"regionType": "Region"
		},
		{
			"name": "Liberia",
			"code2": "LR",
			"code3": "LBR",
			"regionType": "Region"
		},
		{
			"name": "Liechtenstein",
			"code2": "LI",
			"code3": "LIE",
			"regionType": "Region"
		},
		{
			"name": "Lithuania",
			"code2": "LT",
			"code3": "LTU",
			"regionType": "Region"
		},
		{
			"name": "Luxembourg",
			"code2": "LU",
			"code3": "LUX",
			"regionType": "Region"
		},
		{
			"name": "Macao",
			"code2": "MO",
			"code3": "MAC",
			"regionType": "Region"
		},
		{
			"name": "Macedonia, Republic Of",
			"code2": "MK",
			"code3": "MKD",
			"regionType": "Region"
		},
		{
			"name": "Madagascar",
			"code2": "MG",
			"code3": "MDG",
			"regionType": "Region"
		},
		{
			"name": "Malaysia",
			"code2": "MY",
			"code3": "MYS",
			"regions": [
				{
					"name": "Johor",
					"code": "JHR"
				},
				{
					"name": "Kedah",
					"code": "KDH"
				},
				{
					"name": "Kelantan",
					"code": "KTN"
				},
				{
					"name": "Kuala Lumpur",
					"code": "KUL"
				},
				{
					"name": "Labuan",
					"code": "LBN"
				},
				{
					"name": "Melaka",
					"code": "MLK"
				},
				{
					"name": "Negeri Sembilan",
					"code": "NSN"
				},
				{
					"name": "Pahang",
					"code": "PHG"
				},
				{
					"name": "Perak",
					"code": "PRK"
				},
				{
					"name": "Perlis",
					"code": "PLS"
				},
				{
					"name": "Pulau Pinang",
					"code": "PNG"
				},
				{
					"name": "Putrajaya",
					"code": "PJY"
				},
				{
					"name": "Sabah",
					"code": "SBH"
				},
				{
					"name": "Sarawak",
					"code": "SWK"
				},
				{
					"name": "Selangor",
					"code": "SGR"
				},
				{
					"name": "Terengganu",
					"code": "TRG"
				}
			],
			"regionType": "State/Territory"
		},
		{
			"name": "Maldives",
			"code2": "MV",
			"code3": "MDV",
			"regionType": "Region"
		},
		{
			"name": "Malta",
			"code2": "MT",
			"code3": "MLT",
			"regionType": "Region"
		},
		{
			"name": "Mauritius",
			"code2": "MU",
			"code3": "MUS",
			"regionType": "Region"
		},
		{
			"name": "Mexico",
			"code2": "MX",
			"code3": "MEX",
			"regions": [
				{
					"name": "Aguascalientes",
					"code": "AGS"
				},
				{
					"name": "Baja California",
					"code": "BC"
				},
				{
					"name": "Baja California Sur",
					"code": "BCS"
				},
				{
					"name": "Chihuahua",
					"code": "CHIH"
				},
				{
					"name": "Colima",
					"code": "COL"
				},
				{
					"name": "Campeche",
					"code": "CAMP"
				},
				{
					"name": "Coahuila",
					"code": "COAH"
				},
				{
					"name": "Chiapas",
					"code": "CHIS"
				},
				{
					"name": "Distrito Federal",
					"code": "DF"
				},
				{
					"name": "Durango",
					"code": "DGO"
				},
				{
					"name": "Guerrero",
					"code": "GRO"
				},
				{
					"name": "Guanajuato",
					"code": "GTO"
				},
				{
					"name": "Hidalgo",
					"code": "HGO"
				},
				{
					"name": "Jalisco",
					"code": "JAL"
				},
				{
					"name": "Michoacán",
					"code": "MICH"
				},
				{
					"name": "Morelos",
					"code": "MOR"
				},
				{
					"name": "México",
					"code": "MEX"
				},
				{
					"name": "Nayarit",
					"code": "NAY"
				},
				{
					"name": "Nuevo León",
					"code": "NL"
				},
				{
					"name": "Oaxaca",
					"code": "OAx"
				},
				{
					"name": "Puebla",
					"code": "PUE"
				},
				{
					"name": "Quintana Roo",
					"code": "Q ROO"
				},
				{
					"name": "Querétaro",
					"code": "QRO"
				},
				{
					"name": "Sinaloa",
					"code": "SIN"
				},
				{
					"name": "San Luis Potosí",
					"code": "SLP"
				},
				{
					"name": "Sonora",
					"code": "SON"
				},
				{
					"name": "Tabasco",
					"code": "TAB"
				},
				{
					"name": "Tlaxcala",
					"code": "TLAX"
				},
				{
					"name": "Tamaulipas",
					"code": "TAMPS"
				},
				{
					"name": "Veracruz",
					"code": "VER"
				},
				{
					"name": "Yucatán",
					"code": "YUC"
				},
				{
					"name": "Zacatecas",
					"code": "ZAC"
				}
			],
			"regionType": "State"
		},
		{
			"name": "Moldova, Republic of",
			"code2": "MD",
			"code3": "MDA",
			"regionType": "Region"
		},
		{
			"name": "Monaco",
			"code2": "MC",
			"code3": "MCO",
			"regionType": "Region"
		},
		{
			"name": "Mongolia",
			"code2": "MN",
			"code3": "MNG",
			"regionType": "Region"
		},
		{
			"name": "Montenegro",
			"code2": "ME",
			"code3": "MNE",
			"regionType": "Region"
		},
		{
			"name": "Morocco",
			"code2": "MA",
			"code3": "MAR",
			"regionType": "Region"
		},
		{
			"name": "Mozambique",
			"code2": "MZ",
			"code3": "MOZ",
			"regionType": "Region"
		},
		{
			"name": "Myanmar",
			"code2": "MM",
			"code3": "MMR",
			"regionType": "Region"
		},
		{
			"name": "Namibia",
			"code2": "NA",
			"code3": "NAM",
			"regionType": "Region"
		},
		{
			"name": "Nepal",
			"code2": "NP",
			"code3": "NPL",
			"regionType": "Region"
		},
		{
			"name": "Netherlands Antilles",
			"code2": "AN",
			"code3": "ANT",
			"regionType": "Region"
		},
		{
			"name": "Netherlands",
			"code2": "NL",
			"code3": "NLD",
			"regionType": "Region"
		},
		{
			"name": "New Zealand",
			"code2": "NZ",
			"code3": "NZL",
			"regions": [
				{
					"name": "Auckland",
					"code": "AUK"
				},
				{
					"name": "Bay of Plenty",
					"code": "BOP"
				},
				{
					"name": "Canterbury",
					"code": "CAN"
				},
				{
					"name": "Gisborne",
					"code": "GIS"
				},
				{
					"name": "Hawke's Bay",
					"code": "HKB"
				},
				{
					"name": "Manawatu-Wanganui",
					"code": "MWT"
				},
				{
					"name": "Marlborough",
					"code": "MBH"
				},
				{
					"name": "Nelson",
					"code": "NSN"
				},
				{
					"name": "Northland",
					"code": "NTL"
				},
				{
					"name": "Otago",
					"code": "OTA"
				},
				{
					"name": "Southland",
					"code": "STL"
				},
				{
					"name": "Taranaki",
					"code": "TKI"
				},
				{
					"name": "Tasman",
					"code": "TAS"
				},
				{
					"name": "Waikato",
					"code": "WKO"
				},
				{
					"name": "Wellington",
					"code": "WGN"
				},
				{
					"name": "West Coast",
					"code": "WTC"
				}
			],
			"regionType": "Region"
		},
		{
			"name": "Nicaragua",
			"code2": "NI",
			"code3": "NIC",
			"regionType": "Region"
		},
		{
			"name": "Niger",
			"code2": "NE",
			"code3": "NER",
			"regionType": "Region"
		},
		{
			"name": "Nigeria",
			"code2": "NG",
			"code3": "NGA",
			"regionType": "State"
		},
		{
			"name": "Norway",
			"code2": "NO",
			"code3": "NOR",
			"regionType": "Region"
		},
		{
			"name": "Oman",
			"code2": "OM",
			"code3": "OMN",
			"regionType": "Region"
		},
		{
			"name": "Pakistan",
			"code2": "PK",
			"code3": "PAK",
			"regionType": "Region"
		},
		{
			"name": "Palestinian Territory, Occupied",
			"code2": "PS",
			"code3": "PSE",
			"regionType": "Region"
		},
		{
			"name": "Panama",
			"code2": "PA",
			"code3": "PAN",
			"regionType": "Region"
		},
		{
			"name": "Papua New Guinea",
			"code2": "PG",
			"code3": "PNG",
			"regionType": "Region"
		},
		{
			"name": "Paraguay",
			"code2": "PY",
			"code3": "PRY",
			"regionType": "Region"
		},
		{
			"name": "Peru",
			"code2": "PE",
			"code3": "PER",
			"regionType": "Region"
		},
		{
			"name": "Philippines",
			"code2": "PH",
			"code3": "PHL",
			"regionType": "Region"
		},
		{
			"name": "Poland",
			"code2": "PL",
			"code3": "POL",
			"regionType": "Region"
		},
		{
			"name": "Portugal",
			"code2": "PT",
			"code3": "PRT",
			"regions": [
				{
					"name": "Aveiro",
					"code": "PT-01"
				},
				{
					"name": "Beja",
					"code": "PT-02"
				},
				{
					"name": "Braga",
					"code": "PT-03"
				},
				{
					"name": "Bragança",
					"code": "PT-04"
				},
				{
					"name": "Castelo Branco",
					"code": "PT-05"
				},
				{
					"name": "Coimbra",
					"code": "PT-06"
				},
				{
					"name": "Évora",
					"code": "PT-07"
				},
				{
					"name": "Faro",
					"code": "PT-08"
				},
				{
					"name": "Guarda",
					"code": "PT-09"
				},
				{
					"name": "Leiria",
					"code": "PT-10"
				},
				{
					"name": "Lisboa",
					"code": "PT-11"
				},
				{
					"name": "Portalegre",
					"code": "PT-12"
				},
				{
					"name": "Porto",
					"code": "PT-13"
				},
				{
					"name": "Santarém",
					"code": "PT-14"
				},
				{
					"name": "Setúbal",
					"code": "PT-15"
				},
				{
					"name": "Viana do Castelo",
					"code": "PT-16"
				},
				{
					"name": "Vila Real",
					"code": "PT-17"
				},
				{
					"name": "Viseu",
					"code": "PT-18"
				},
				{
					"name": "Açores",
					"code": "PT-20"
				},
				{
					"name": "Madeira",
					"code": "PT-30"
				}
			],
			"regionType": "Region"
		},
		{
			"name": "Qatar",
			"code2": "QA",
			"code3": "QAT",
			"regionType": "Region"
		},
		{
			"name": "Reunion",
			"code2": "RE",
			"code3": "REU",
			"regionType": "Region"
		},
		{
			"name": "Romania",
			"code2": "RO",
			"code3": "ROU",
			"regionType": "Region"
		},
		{
			"name": "Russia",
			"code2": "RU",
			"code3": "RUS",
			"regions": [
				{
					"name": "Republic of Adygeya",
					"code": "AD"
				},
				{
					"name": "Altai Republic",
					"code": "AL"
				},
				{
					"name": "Altai Krai",
					"code": "ALT"
				},
				{
					"name": "Amur Oblast",
					"code": "AMU"
				},
				{
					"name": "Arkhangelsk Oblast",
					"code": "ARK"
				},
				{
					"name": "Astrakhan Oblast",
					"code": "AST"
				},
				{
					"name": "Republic of Bashkortostan",
					"code": "BA"
				},
				{
					"name": "Belgorod Oblast",
					"code": "BEL"
				},
				{
					"name": "Bryansk Oblast",
					"code": "BRY"
				},
				{
					"name": "Republic of Buryatia",
					"code": "BU"
				},
				{
					"name": "Chechen Republic",
					"code": "CE"
				},
				{
					"name": "Chelyabinsk Oblast",
					"code": "CHE"
				},
				{
					"name": "Chukotka Autonomous Okrug",
					"code": "CHU"
				},
				{
					"name": "Chuvash Republic",
					"code": "CU"
				},
				{
					"name": "Republic of Dagestan",
					"code": "DA"
				},
				{
					"name": "Republic of Ingushetia",
					"code": "IN"
				},
				{
					"name": "Irkutsk Oblast",
					"code": "IRK"
				},
				{
					"name": "Ivanovo Oblast",
					"code": "IVA"
				},
				{
					"name": "Kamchatka Krai",
					"code": "KAM"
				},
				{
					"name": "Kabardino-Balkarian Republic",
					"code": "KB"
				},
				{
					"name": "Kaliningrad Oblast",
					"code": "KGD"
				},
				{
					"name": "Republic of Kalmykia",
					"code": "KL"
				},
				{
					"name": "Kaluga Oblast",
					"code": "KLU"
				},
				{
					"name": "Karachay–Cherkess Republic",
					"code": "KC"
				},
				{
					"name": "Republic of Karelia",
					"code": "KR"
				},
				{
					"name": "Kemerovo Oblast",
					"code": "KEM"
				},
				{
					"name": "Khabarovsk Krai",
					"code": "KHA"
				},
				{
					"name": "Republic of Khakassia",
					"code": "KK"
				},
				{
					"name": "Khanty-Mansi Autonomous Okrug",
					"code": "KHM"
				},
				{
					"name": "Kirov Oblast",
					"code": "KIR"
				},
				{
					"name": "Komi Republic",
					"code": "KO"
				},
				{
					"name": "Kostroma Oblast",
					"code": "KOS"
				},
				{
					"name": "Krasnodar Krai",
					"code": "KDA"
				},
				{
					"name": "Krasnoyarsk Krai",
					"code": "KYA"
				},
				{
					"name": "Kurgan Oblast",
					"code": "KGN"
				},
				{
					"name": "Kursk Oblast",
					"code": "KRS"
				},
				{
					"name": "Leningrad Oblast",
					"code": "LEN"
				},
				{
					"name": "Lipetsk Oblast",
					"code": "LIP"
				},
				{
					"name": "Magadan Oblast",
					"code": "MAG"
				},
				{
					"name": "Mari El Republic",
					"code": "ME"
				},
				{
					"name": "Republic of Mordovia",
					"code": "MO"
				},
				{
					"name": "Moscow Oblast",
					"code": "MOS"
				},
				{
					"name": "Moscow",
					"code": "MOW"
				},
				{
					"name": "Murmansk Oblast",
					"code": "MUR"
				},
				{
					"name": "Nizhny Novgorod Oblast",
					"code": "NIZ"
				},
				{
					"name": "Novgorod Oblast",
					"code": "NGR"
				},
				{
					"name": "Novosibirsk Oblast",
					"code": "NVS"
				},
				{
					"name": "Omsk Oblast",
					"code": "OMS"
				},
				{
					"name": "Orenburg Oblast",
					"code": "ORE"
				},
				{
					"name": "Oryol Oblast",
					"code": "ORL"
				},
				{
					"name": "Penza Oblast",
					"code": "PNZ"
				},
				{
					"name": "Perm Krai",
					"code": "PER"
				},
				{
					"name": "Primorsky Krai",
					"code": "PRI"
				},
				{
					"name": "Pskov Oblast",
					"code": "PSK"
				},
				{
					"name": "Rostov Oblast",
					"code": "ROS"
				},
				{
					"name": "Ryazan Oblast",
					"code": "RYA"
				},
				{
					"name": "Sakha Republic (Yakutia)",
					"code": "SA"
				},
				{
					"name": "Sakhalin Oblast",
					"code": "SAK"
				},
				{
					"name": "Samara Oblast",
					"code": "SAM"
				},
				{
					"name": "Saint Petersburg",
					"code": "SPE"
				},
				{
					"name": "Saratov Oblast",
					"code": "SAR"
				},
				{
					"name": "Republic of North Ossetia–Alania",
					"code": "SE"
				},
				{
					"name": "Smolensk Oblast",
					"code": "SMO"
				},
				{
					"name": "Stavropol Krai",
					"code": "STA"
				},
				{
					"name": "Sverdlovsk Oblast",
					"code": "SVE"
				},
				{
					"name": "Tambov Oblast",
					"code": "TAM"
				},
				{
					"name": "Republic of Tatarstan",
					"code": "TA"
				},
				{
					"name": "Tomsk Oblast",
					"code": "TOM"
				},
				{
					"name": "Tula Oblast",
					"code": "TUL"
				},
				{
					"name": "Tver Oblast",
					"code": "TVE"
				},
				{
					"name": "Tyumen Oblast",
					"code": "TYU"
				},
				{
					"name": "Tyva Republic",
					"code": "TY"
				},
				{
					"name": "Udmurtia",
					"code": "UD"
				},
				{
					"name": "Ulyanovsk Oblast",
					"code": "ULY"
				},
				{
					"name": "Vladimir Oblast",
					"code": "VLA"
				},
				{
					"name": "Volgograd Oblast",
					"code": "VGG"
				},
				{
					"name": "Vologda Oblast",
					"code": "VLG"
				},
				{
					"name": "Voronezh Oblast",
					"code": "VOR"
				},
				{
					"name": "Yamalo-Nenets Autonomous Okrug",
					"code": "YAN"
				},
				{
					"name": "Yaroslavl Oblast",
					"code": "YAR"
				},
				{
					"name": "Jewish Autonomous Oblast",
					"code": "YEV"
				}
			],
			"regionType": "Region"
		},
		{
			"name": "Rwanda",
			"code2": "RW",
			"code3": "RWA",
			"regionType": "Region"
		},
		{
			"name": "Saint Kitts And Nevis",
			"code2": "KN",
			"code3": "KNA",
			"regionType": "Region"
		},
		{
			"name": "Saint Lucia",
			"code2": "LC",
			"code3": "LCA",
			"regionType": "Region"
		},
		{
			"name": "Saint Martin",
			"code2": "MF",
			"code3": "MAF",
			"regionType": "Region"
		},
		{
			"name": "Sao Tome And Principe",
			"code2": "ST",
			"code3": "STP",
			"regionType": "Region"
		},
		{
			"name": "Samoa",
			"code2": "WS",
			"code3": "WSM",
			"regionType": "Region"
		},
		{
			"name": "Saudi Arabia",
			"code2": "SA",
			"code3": "SAU",
			"regionType": "Region"
		},
		{
			"name": "Senegal",
			"code2": "SN",
			"code3": "SEN",
			"regionType": "Region"
		},
		{
			"name": "Serbia",
			"code2": "RS",
			"code3": "SRB",
			"regionType": "Region"
		},
		{
			"name": "Seychelles",
			"code2": "SC",
			"code3": "SYC",
			"regionType": "Region"
		},
		{
			"name": "Singapore",
			"code2": "SG",
			"code3": "SGP",
			"regionType": "Region"
		},
		{
			"name": "Sint Maarten",
			"code2": "SX",
			"code3": "SXM",
			"regionType": "Region"
		},
		{
			"name": "Slovakia",
			"code2": "SK",
			"code3": "SVK",
			"regionType": "Region"
		},
		{
			"name": "Slovenia",
			"code2": "SI",
			"code3": "SVN",
			"regionType": "Region"
		},
		{
			"name": "South Africa",
			"code2": "ZA",
			"code3": "ZAF",
			"regions": [
				{
					"name": "Eastern Cape",
					"code": "EC"
				},
				{
					"name": "Free State",
					"code": "FS"
				},
				{
					"name": "Gauteng",
					"code": "GT"
				},
				{
					"name": "KwaZulu-Natal",
					"code": "NL"
				},
				{
					"name": "Limpopo",
					"code": "LP"
				},
				{
					"name": "Mpumalanga",
					"code": "MP"
				},
				{
					"name": "Northern Cape",
					"code": "NC"
				},
				{
					"name": "North West",
					"code": "NW"
				},
				{
					"name": "Western Cape",
					"code": "WC"
				}
			],
			"regionType": "Province"
		},
		{
			"name": "South Korea",
			"code2": "KR",
			"code3": "KOR",
			"regionType": "Region"
		},
		{
			"name": "Spain",
			"code2": "ES",
			"code3": "ESP",
			"regions": [
				{
					"name": "A Coruña",
					"code": "C"
				},
				{
					"name": "Álava",
					"code": "VI"
				},
				{
					"name": "Albacete",
					"code": "AB"
				},
				{
					"name": "Alicante",
					"code": "A"
				},
				{
					"name": "Almería",
					"code": "AL"
				},
				{
					"name": "Asturias",
					"code": "O"
				},
				{
					"name": "Ávila",
					"code": "AV"
				},
				{
					"name": "Badajoz",
					"code": "BA"
				},
				{
					"name": "Balears",
					"code": "PM"
				},
				{
					"name": "Barcelona",
					"code": "B"
				},
				{
					"name": "Burgos",
					"code": "BU"
				},
				{
					"name": "Cáceres",
					"code": "CC"
				},
				{
					"name": "Cádiz",
					"code": "CA"
				},
				{
					"name": "Cantabria",
					"code": "S"
				},
				{
					"name": "Castellón",
					"code": "CS"
				},
				{
					"name": "Ceuta",
					"code": "CE"
				},
				{
					"name": "Ciudad Real",
					"code": "CR"
				},
				{
					"name": "Córdoba",
					"code": "CO"
				},
				{
					"name": "Cuenca",
					"code": "CU"
				},
				{
					"name": "Girona",
					"code": "GI"
				},
				{
					"name": "Granada",
					"code": "GR"
				},
				{
					"name": "Guadalajara",
					"code": "GU"
				},
				{
					"name": "Guipúzcoa",
					"code": "SS"
				},
				{
					"name": "Huelva",
					"code": "H"
				},
				{
					"name": "Huesca",
					"code": "HU"
				},
				{
					"name": "Jaén",
					"code": "J"
				},
				{
					"name": "La Rioja",
					"code": "LO"
				},
				{
					"name": "Las Palmas",
					"code": "GC"
				},
				{
					"name": "León",
					"code": "LE"
				},
				{
					"name": "Lleida",
					"code": "L"
				},
				{
					"name": "Lugo",
					"code": "LU"
				},
				{
					"name": "Madrid",
					"code": "M"
				},
				{
					"name": "Málaga",
					"code": "MA"
				},
				{
					"name": "Melilla",
					"code": "ML"
				},
				{
					"name": "Murcia",
					"code": "MU"
				},
				{
					"name": "Navarra",
					"code": "NA"
				},
				{
					"name": "Ourense",
					"code": "OR"
				},
				{
					"name": "Palencia",
					"code": "P"
				},
				{
					"name": "Pontevedra",
					"code": "PO"
				},
				{
					"name": "Salamanca",
					"code": "SA"
				},
				{
					"name": "Santa Cruz de Tenerife",
					"code": "TF"
				},
				{
					"name": "Segovia",
					"code": "SG"
				},
				{
					"name": "Sevilla",
					"code": "SE"
				},
				{
					"name": "Soria",
					"code": "SO"
				},
				{
					"name": "Tarragona",
					"code": "T"
				},
				{
					"name": "Teruel",
					"code": "TE"
				},
				{
					"name": "Toledo",
					"code": "TO"
				},
				{
					"name": "Valencia",
					"code": "V"
				},
				{
					"name": "Valladolid",
					"code": "VA"
				},
				{
					"name": "Vizcaya",
					"code": "BI"
				},
				{
					"name": "Zamora",
					"code": "ZA"
				},
				{
					"name": "Zaragoza",
					"code": "Z"
				}
			],
			"regionType": "Province"
		},
		{
			"name": "Sri Lanka",
			"code2": "LK",
			"code3": "LKA",
			"regionType": "Region"
		},
		{
			"name": "St. Vincent",
			"code2": "VC",
			"code3": "VCT",
			"regionType": "Region"
		},
		{
			"name": "Suriname",
			"code2": "SR",
			"code3": "SUR",
			"regionType": "Region"
		},
		{
			"name": "Sweden",
			"code2": "SE",
			"code3": "SWE",
			"regionType": "Region"
		},
		{
			"name": "Switzerland",
			"code2": "CH",
			"code3": "CHE",
			"regionType": "Region"
		},
		{
			"name": "Syria",
			"code2": "SY",
			"code3": "SYR",
			"regionType": "Region"
		},
		{
			"name": "Taiwan",
			"code2": "TW",
			"code3": "TWN",
			"regionType": "Region"
		},
		{
			"name": "Thailand",
			"code2": "TH",
			"code3": "THA",
			"regionType": "Region"
		},
		{
			"name": "Tanzania, United Republic Of",
			"code2": "TZ",
			"code3": "TZA",
			"regionType": "Region"
		},
		{
			"name": "Trinidad and Tobago",
			"code2": "TT",
			"code3": "TTO",
			"regionType": "Region"
		},
		{
			"name": "Tunisia",
			"code2": "TN",
			"code3": "TUN",
			"regionType": "Region"
		},
		{
			"name": "Turkey",
			"code2": "TR",
			"code3": "TUR",
			"regionType": "Region"
		},
		{
			"name": "Turkmenistan",
			"code2": "TM",
			"code3": "TKM",
			"regionType": "Region"
		},
		{
			"name": "Turks and Caicos Islands",
			"code2": "TC",
			"code3": "TCA",
			"regionType": "Region"
		},
		{
			"name": "Uganda",
			"code2": "UG",
			"code3": "UGA",
			"regionType": "Region"
		},
		{
			"name": "Ukraine",
			"code2": "UA",
			"code3": "UKR",
			"regionType": "Region"
		},
		{
			"name": "United Arab Emirates",
			"code2": "AE",
			"code3": "ARE",
			"regions": [
				{
					"name": "Abu Dhabi",
					"code": "AZ"
				},
				{
					"name": "Ajman",
					"code": "AJ"
				},
				{
					"name": "Dubai",
					"code": "DU"
				},
				{
					"name": "Fujairah",
					"code": "FU"
				},
				{
					"name": "Ras al-Khaimah",
					"code": "RK"
				},
				{
					"name": "Sharjah",
					"code": "SH"
				},
				{
					"name": "Umm al-Quwain",
					"code": "UQ"
				}
			],
			"regionType": "Emirate"
		},
		{
			"name": "Uruguay",
			"code2": "UY",
			"code3": "URY",
			"regionType": "Region"
		},
		{
			"name": "Uzbekistan",
			"code2": "UZ",
			"code3": "UZB",
			"regionType": "Province"
		},
		{
			"name": "Vanuatu",
			"code2": "VU",
			"code3": "VUT",
			"regionType": "Region"
		},
		{
			"name": "Venezuela",
			"code2": "VE",
			"code3": "VEN",
			"regionType": "Region"
		},
		{
			"name": "Vietnam",
			"code2": "VN",
			"code3": "VNM",
			"regionType": "Region"
		},
		{
			"name": "Virgin Islands, British",
			"code2": "VG",
			"code3": "VGB",
			"regionType": "Region"
		},
		{
			"name": "Yemen",
			"code2": "YE",
			"code3": "YEM",
			"regionType": "Region"
		},
		{
			"name": "Zambia",
			"code2": "ZM",
			"code3": "ZMB",
			"regionType": "Region"
		},
		{
			"name": "Zimbabwe",
			"code2": "ZW",
			"code3": "ZWE",
			"regionType": "Region"
		},
		{
			"name": "Afghanistan",
			"code2": "AF",
			"code3": "AFG",
			"regionType": "Region"
		},
		{
			"name": "Aland Islands",
			"code2": "ALA",
			"code3": "AX",
			"regionType": "Region"
		},
		{
			"name": "Anguilla",
			"code2": "AI",
			"code3": "AIA",
			"regionType": "Region"
		},
		{
			"name": "Benin",
			"code2": "BJ",
			"code3": "BEN",
			"regionType": "Region"
		},
		{
			"name": "Bouvet Island",
			"code2": "BV",
			"code3": "BVT",
			"regionType": "Region"
		},
		{
			"name": "British Indian Ocean Territory",
			"code2": "IO",
			"code3": "IOT",
			"regionType": "Region"
		},
		{
			"name": "Burkina Faso",
			"code2": "BF",
			"code3": "BFA",
			"regionType": "Region"
		},
		{
			"name": "Burundi",
			"code2": "BI",
			"code3": "BDI",
			"regionType": "Region"
		},
		{
			"name": "Cape Verde",
			"code2": "CV",
			"code3": "CPV",
			"regionType": "Region"
		},
		{
			"name": "Central African Republic",
			"code2": "CF",
			"code3": "CAF",
			"regionType": "Region"
		},
		{
			"name": "Chad",
			"code2": "TD",
			"code3": "TCD",
			"regionType": "Region"
		},
		{
			"name": "Christmas Island",
			"code2": "CX",
			"code3": "CXR",
			"regionType": "Region"
		},
		{
			"name": "Cocos (Keeling) Islands",
			"code2": "CC",
			"code3": "CCK",
			"regionType": "Region"
		},
		{
			"name": "Congo, The Democratic Republic Of The",
			"code2": "CD",
			"code3": "COD",
			"regionType": "Region"
		},
		{
			"name": "Cook Islands",
			"code2": "CK",
			"code3": "COK",
			"regionType": "Region"
		},
		{
			"name": "Cuba",
			"code2": "CU",
			"code3": "CUB",
			"regionType": "Region"
		},
		{
			"name": "Djibouti",
			"code2": "DJ",
			"code3": "DJI",
			"regionType": "Region"
		},
		{
			"name": "Equatorial Guinea",
			"code2": "GQ",
			"code3": "GNQ",
			"regionType": "Region"
		},
		{
			"name": "Eritrea",
			"code2": "ER",
			"code3": "ERI",
			"regionType": "Region"
		},
		{
			"name": "Falkland Islands (Malvinas)",
			"code2": "FK",
			"code3": "FLK",
			"regionType": "Region"
		},
		{
			"name": "French Guiana",
			"code2": "GF",
			"code3": "GUF",
			"regionType": "Region"
		},
		{
			"name": "French Southern Territories",
			"code2": "TF",
			"code3": "ATF",
			"regionType": "Region"
		},
		{
			"name": "Gabon",
			"code2": "GA",
			"code3": "GAB",
			"regionType": "Region"
		},
		{
			"name": "Guinea",
			"code2": "GN",
			"code3": "GIN",
			"regionType": "Region"
		},
		{
			"name": "Guinea Bissau",
			"code2": "GW",
			"code3": "GNB",
			"regionType": "Region"
		},
		{
			"name": "Heard Island And Mcdonald Islands",
			"code2": "HM",
			"code3": "HMD",
			"regionType": "Region"
		},
		{
			"name": "Holy See (Vatican City State)",
			"code2": "VA",
			"code3": "VAT",
			"regionType": "Region"
		},
		{
			"name": "Iran, Islamic Republic Of",
			"code2": "IR",
			"code3": "IRN",
			"regionType": "Region"
		},
		{
			"name": "Iraq",
			"code2": "IQ",
			"code3": "IRQ",
			"regionType": "Region"
		},
		{
			"name": "Kiribati",
			"code2": "KI",
			"code3": "KIR",
			"regionType": "Region"
		},
		{
			"name": "Korea, Democratic People's Republic Of",
			"code2": "KP",
			"code3": "PRK",
			"regionType": "Region"
		},
		{
			"name": "Lao People's Democratic Republic",
			"code2": "LA",
			"code3": "LAO",
			"regionType": "Region"
		},
		{
			"name": "Lesotho",
			"code2": "LS",
			"code3": "LSO",
			"regionType": "Region"
		},
		{
			"name": "Libyan Arab Jamahiriya",
			"code2": "LY",
			"code3": "LBY",
			"regionType": "Region"
		},
		{
			"name": "Malawi",
			"code2": "MW",
			"code3": "MWI",
			"regionType": "Region"
		},
		{
			"name": "Mali",
			"code2": "ML",
			"code3": "MLI",
			"regionType": "Region"
		},
		{
			"name": "Martinique",
			"code2": "MQ",
			"code3": "MTQ",
			"regionType": "Region"
		},
		{
			"name": "Mauritania",
			"code2": "MR",
			"code3": "MRT",
			"regionType": "Region"
		},
		{
			"name": "Mayotte",
			"code2": "YT",
			"code3": "MYT",
			"regionType": "Region"
		},
		{
			"name": "Montserrat",
			"code2": "MS",
			"code3": "MSR",
			"regionType": "Region"
		},
		{
			"name": "Nauru",
			"code2": "NR",
			"code3": "NRU",
			"regionType": "Region"
		},
		{
			"name": "New Caledonia",
			"code2": "NC",
			"code3": "NCL",
			"regionType": "Region"
		},
		{
			"name": "Niue",
			"code2": "NU",
			"code3": "NIU",
			"regionType": "Region"
		},
		{
			"name": "Norfolk Island",
			"code2": "NF",
			"code3": "NFK",
			"regionType": "Region"
		},
		{
			"name": "Pitcairn",
			"code2": "PN",
			"code3": "PCN",
			"regionType": "Region"
		},
		{
			"name": "Saint Barthélemy",
			"code2": "BL",
			"code3": "BLM",
			"regionType": "Region"
		},
		{
			"name": "Saint Helena",
			"code2": "SH",
			"code3": "SHN",
			"regionType": "Region"
		},
		{
			"name": "Saint Pierre And Miquelon",
			"code2": "PM",
			"code3": "SPM",
			"regionType": "Region"
		},
		{
			"name": "San Marino",
			"code2": "SM",
			"code3": "SMR",
			"regionType": "Region"
		},
		{
			"name": "Sierra Leone",
			"code2": "SL",
			"code3": "SLE",
			"regionType": "Region"
		},
		{
			"name": "Solomon Islands",
			"code2": "SB",
			"code3": "SLB",
			"regionType": "Region"
		},
		{
			"name": "Somalia",
			"code2": "SO",
			"code3": "SOM",
			"regionType": "Region"
		},
		{
			"name": "South Georgia And The South Sandwich Islands",
			"code2": "GS",
			"code3": "SGS",
			"regionType": "Region"
		},
		{
			"name": "Sudan",
			"code2": "SD",
			"code3": "SDN",
			"regionType": "Region"
		},
		{
			"name": "Svalbard And Jan Mayen",
			"code2": "SJ",
			"code3": "SJM",
			"regionType": "Region"
		},
		{
			"name": "Swaziland",
			"code2": "SZ",
			"code3": "SWZ",
			"regionType": "Region"
		},
		{
			"name": "Tajikistan",
			"code2": "TJ",
			"code3": "TAJ",
			"regionType": "Region"
		},
		{
			"name": "Timor Leste",
			"code2": "TL",
			"code3": "TLS",
			"regionType": "Region"
		},
		{
			"name": "Togo",
			"code2": "TG",
			"code3": "TGO",
			"regionType": "Region"
		},
		{
			"name": "Tokelau",
			"code2": "TK",
			"code3": "TKL",
			"regionType": "Region"
		},
		{
			"name": "Tonga",
			"code2": "TO",
			"code3": "TON",
			"regionType": "Region"
		},
		{
			"name": "Tuvalu",
			"code2": "TV",
			"code3": "TUV",
			"regionType": "Region"
		},
		{
			"name": "United States Minor Outlying Islands",
			"code2": "UM",
			"code3": "UMI",
			"regionType": "State"
		},
		{
			"name": "Wallis And Futuna",
			"code2": "WF",
			"code3": "WLF",
			"regionType": "Region"
		},
		{
			"name": "Western Sahara",
			"code2": "EH",
			"code3": "ESH",
			"regionType": "Region"
		}
	];
});