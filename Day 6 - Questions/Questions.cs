using System.Collections.Generic;
using System.Linq;

public class Questions
{
    static public int GetAnyoneSum()
        => Values.Select(v => GetAnyoneCount(v)).Sum();

    static public int GetEveryoneSum()
        => Values.Select(v => GetEveyoneCount(v)).Sum();

    static private int GetAnyoneCount(List<string> groupAnswers)
    {
        var uniques = new HashSet<char>();
        foreach (var personAnswers in groupAnswers)
            foreach (var answer in personAnswers)
                uniques.Add(answer);

        return uniques.Count;
    }

    static private int GetEveyoneCount(List<string> groupAnswers)
    {
        var filter = new List<char>();
        filter.AddRange(groupAnswers.First());
        
        foreach (var personAnswers in groupAnswers)
            filter = personAnswers
                .Where(pa => filter.Contains(pa))
                .ToList();

        return filter.Count;
    }

    static public List<List<string>> Values = new List<List<string>>
    {
        new List<string> {
        "zvxc",
        "dv",
        "vh",
        "xv",
        "jvem"},

        new List<string> {
        "mxfhdeyikljnz",
        "vwzbjmsrgq"},

        new List<string> {
        "vbtjnh",
        "vhejnbti",
        "vthnjb",
        "tsbhjnv"},

        new List<string> {
        "cbjemdvp",
        "jdvuylp",
        "ofpjaqgsrxvdn",
        "dzvpbj",
        "vbktpdj"},

        new List<string> {
        "knyvdhsemu",
        "sdyumkgvh",
        "muyvdskh",
        "syhkuvmd",
        "dvmykshu"},

        new List<string> {
        "rmantzovbsuxiljkchfgdqew",
        "vficzosgwlmrdtqkueajbnxh",
        "davqlntibxhojesrkugpzmwfc"},

        new List<string> {
        "ifdmgrcve",
        "bkqal",
        "gxcwvt"},

        new List<string> {
        "xeiokblasch",
        "toxjlsbnciha",
        "sovycihlabx",
        "ximhylocabs",
        "hixsolabc"},

        new List<string> {
        "iyfkvmpolta",
        "thpqodzifxc"},

        new List<string> {
        "iexunoyhwv",
        "ovweiyuhxn",
        "uhojnvyxeiw"},

        new List<string> {
        "vbncamhzojtqrlpse",
        "wujapcidzx",
        "zxjiuacwkgfdp"},

        new List<string> {
        "bygewhzsnqvxaoc",
        "sngqvepohyzdwxcab",
        "yoxwzmgvesbqhcua",
        "cqtbwehiavofzsgylx"},

        new List<string> {
        "rxanvifdup",
        "xuvpnfria",
        "rfnpvixua",
        "npiuxfarv",
        "vnifuxpra"},

        new List<string> {
        "ysrktfhc",
        "trfkcysh",
        "ktfycrsh",
        "rhckstyf"},

        new List<string> {
        "nq",
        "kqin",
        "nq",
        "qn",
        "nq"},

        new List<string> {
        "yorbujt",
        "evqkmscp"},

        new List<string> {
        "fitkprweso",
        "fszkmoretwpi",
        "ptyoswrekim",
        "iuwbrsgeockpajt",
        "yewksitpro"},

        new List<string> {
        "gtyaemflvc",
        "fyvlecgmati",
        "vgfyclemta"},

        new List<string> {
        "k",
        "tj",
        "uebpy",
        "vyma",
        "ibdy"},

        new List<string> {
        "p",
        "p",
        "p"},

        new List<string> {
        "r",
        "r",
        "lya",
        "b",
        "r"},

        new List<string> {
        "cht",
        "hct",
        "cth",
        "tch",
        "hct"},

        new List<string> {
        "qdxpas",
        "dpxsha",
        "parlyekqsx",
        "sxuotvpwaz",
        "jqxspymae"},

        new List<string> {
        "ngycrw",
        "wgnv",
        "sngw"},

        new List<string> {
        "psb",
        "us",
        "s",
        "s",
        "s"},

        new List<string> {
        "xnodk",
        "dxok",
        "tkxdo"},

        new List<string> {
        "n",
        "n",
        "n"},

        new List<string> {
        "tysg",
        "gtys",
        "syg",
        "eghslqry",
        "gyts"},

        new List<string> {
        "qliwfgatpbdcy",
        "aqwflpdygtcb",
        "ypdcglqtwabf",
        "dwgcytlbapqf"},

        new List<string> {
        "nwvlqbusparkf",
        "jhfbvnsqpr",
        "nprqbzfevs",
        "nvrqhboexmspfy"},

        new List<string> {
        "tgr",
        "rgt",
        "gtr"},

        new List<string> {
        "jokfqv",
        "jqk",
        "qjk"},

        new List<string> {
        "fuv",
        "mvuf",
        "vfu"},

        new List<string> {
        "mfw",
        "mwf",
        "fmw",
        "fmw"},

        new List<string> {
        "kt",
        "jp"},

        new List<string> {
        "fklnagsztb",
        "znmtsfalpj"},

        new List<string> {
        "o",
        "qb"},

        new List<string> {
        "h",
        "peqyon",
        "az"},

        new List<string> {
        "n",
        "n"},

        new List<string> {
        "eagyvpmjqxs",
        "qghjsapny",
        "jayrqsgp",
        "qasygjp"},

        new List<string> {
        "yctzugn",
        "ytzcgu",
        "cuytgz"},

        new List<string> {
        "bsfxcygptnlk",
        "xrgocylkifuen"},

        new List<string> {
        "os",
        "wakdftzgh",
        "losn",
        "pn",
        "yp"},

        new List<string> {
        "pie",
        "ei"},

        new List<string> {
        "zpsxeb",
        "tgynfiw",
        "zomvqu",
        "ludk"},

        new List<string> {
        "s",
        "s"},

        new List<string> {
        "wuon",
        "ngw",
        "wn",
        "nw",
        "wn"},

        new List<string> {
        "omrdjkzeb",
        "ncdjuzborkm",
        "zkebrdajmo"},

        new List<string> {
        "tjwaxm",
        "iztwvod",
        "wqt",
        "txwr",
        "wt"},

        new List<string> {
        "lpcbfwiksnvdhrqjut",
        "punidbscqtrwjvkfhl",
        "vrcqukphwjsdbtflin",
        "dvkwlgnhutsrcjbqipf"},

        new List<string> {
        "spzlowvtxdkjinfc",
        "indjlksczxfptvo",
        "czmaxkqnfpbvyshjdliot"},

        new List<string> {
        "aqspmnkxie",
        "trjhfclyogd",
        "zwavixpbeu"},

        new List<string> {
        "mozewqhxkduajgy",
        "mogjekqnfuywzhxda",
        "ozhkaxegyumwqdj",
        "ajdzogmehwqrkyux"},

        new List<string> {
        "qemykxdwipjfa",
        "wdxaplmeyfkiqj",
        "xmqplaekifdwzj",
        "mjixwqzaedkfp",
        "ofpmwxkqjeadi"},

        new List<string> {
        "gpjydb",
        "jpystgkd",
        "gzjdpy",
        "jpgyd",
        "pgjydxh"},

        new List<string> {
        "drockgqe",
        "evanghozbi"},

        new List<string> {
        "kfg",
        "lmfwig",
        "gf",
        "fgsj"},

        new List<string> {
        "zjfnabowyrtmxcgl",
        "ycrjxazgfowbitlnm",
        "hrlwegxokbqzatcnjfmy",
        "jcobagxflzntymwr",
        "grzotfxcymlwavbunj"},

        new List<string> {
        "iamdhcgfynqzos",
        "gyszifcnavdolhqm",
        "qmnwziuropsacdgyh"},

        new List<string> {
        "mwlgcedjtknxsuy",
        "estmwlxuydjkci",
        "hctlyawmxkjseud",
        "jutsxdycwlkmie",
        "mtwqyskbflxjzvcdeuo"},

        new List<string> {
        "sdfwjm",
        "wdsm",
        "ygmqsxdei"},

        new List<string> {
        "u",
        "u",
        "u"},

        new List<string> {
        "dutb",
        "dubt",
        "dutb",
        "tdu",
        "idtu"},

        new List<string> {
        "sqpfzwlvcjtx",
        "haiv",
        "dveyohgb",
        "kuvhm",
        "vik"},

        new List<string> {
        "myzxofuacqsgi",
        "kcamifojuybgqs"},

        new List<string> {
        "mthngfuvykwci",
        "numzktjydcxwio"},

        new List<string> {
        "aspdgtikq",
        "qpkgdtisa",
        "qsgkdptia",
        "pqtuakzdigxs",
        "pdkqtiasg"},

        new List<string> {
        "fityomxdwlscjazhkqp",
        "pmzrcjhvntsobfq",
        "shztqgmrjofcp",
        "jmcngqhoputzefs"},

        new List<string> {
        "whjgxi",
        "wxjoihg",
        "gxjwih"},

        new List<string> {
        "hirpefgxsa",
        "gahfytkz",
        "qdjlzvwfahg",
        "hbaofwgvc"},

        new List<string> {
        "dhcnfgy",
        "fyhdgc",
        "gyctdhf"},

        new List<string> {
        "hbmconewlqgsdzk",
        "jqflbowkdsgc",
        "hwkobvdcsqeilgn",
        "lkcogsaypdwxbq"},

        new List<string> {
        "enzrdgqyu",
        "gvqrtoihlaj",
        "qrgs",
        "qpeurgzkxn"},

        new List<string> {
        "grjdenycstpfbihqo",
        "bfkhlgjrpeysomtvidqc"},

        new List<string> {
        "fxmqujr",
        "wmvghur",
        "dpaeyko"},

        new List<string> {
        "untpyr",
        "urtpyn",
        "apnutry",
        "uptyrn",
        "nustyrepg"},

        new List<string> {
        "ofualjwvq",
        "jeyoqru",
        "yuxqioj",
        "ushtogmdpqjcknb"},

        new List<string> {
        "txeyfwrhl",
        "xrhfytelw"},

        new List<string> {
        "nu",
        "niu"},

        new List<string> {
        "rbmuneiz",
        "neibzurm",
        "mnerbizu"},

        new List<string> {
        "mvtkp",
        "vmpkzt",
        "mpvkt"},

        new List<string> {
        "vakc",
        "erq",
        "b",
        "ijftomxy",
        "zsg"},

        new List<string> {
        "yptu",
        "tpayoub"},

        new List<string> {
        "zsqvdfw",
        "zdswvfq",
        "qzwsfdv",
        "qwzdfsv"},

        new List<string> {
        "pbwvzadgyturskqoefncxhmjl",
        "thazxruyscfqndvpjklbgomwe"},

        new List<string> {
        "xnomrqyzpe",
        "ieudmxroqvbnz"},

        new List<string> {
        "cldqr",
        "ecdqr",
        "ercqd",
        "cdwrq"},

        new List<string> {
        "kwzqcagfxvhjromti",
        "hfsjcqxibokgravztmw",
        "atkrogwjcxvfmzhiq",
        "gwhztkxacfvrimqoj"},

        new List<string> {
        "ztbqli",
        "lobdrhgt",
        "rbltoyh"},

        new List<string> {
        "wfh",
        "fh",
        "hf",
        "fh"},

        new List<string> {
        "rnxkztpmi",
        "umzigxnh",
        "zyxilqnsm"},

        new List<string> {
        "iox",
        "oi"},

        new List<string> {
        "ovugqjpxkshczewmr",
        "smpgxuqzlehkwrvaocj",
        "civspxkmhjwuzegoqr",
        "jergszqvhkxucpomw"},

        new List<string> {
        "mzsrlcfqpuaex",
        "rtxaqnsjczvf",
        "rxsuiazqcdf"},

        new List<string> {
        "zvmltjegwxyfihnsaouq",
        "tmwlgxyhoendfiaq",
        "nrgptwaxleoqfihmyk",
        "tlqhiawmnoyfegx",
        "nhlwfcagqyotxmei"},

        new List<string> {
        "dfoyublxhzajwktrv",
        "olrfzuyphd",
        "fynohuiqemlszr"},

        new List<string> {
        "nmwedzq",
        "qewzdn",
        "zqnedw",
        "qnzwedp",
        "dqezwn"},

        new List<string> {
        "lvugjzmsec",
        "mgnlqsjzudvw"},

        new List<string> {
        "yasmrkxvnpch",
        "yxabnvhpskrdmcl",
        "hvnkarsyxpmc",
        "myhvconkpaxrs"},

        new List<string> {
        "pfelbgs",
        "a",
        "vriu"},

        new List<string> {
        "vfy",
        "oj",
        "izagebph",
        "ck",
        "mv"},

        new List<string> {
        "hn",
        "h",
        "bjhtr",
        "h",
        "h"},

        new List<string> {
        "saixpwbhqdverzcyfnktluo",
        "kuqtfhewypnrbgvcdliozxs",
        "deiyptgkwrzqlohbvxfucsn",
        "bynqihpjwecotlfvzusdrkx"},

        new List<string> {
        "yxvfnlwz",
        "ecvwlfbgnz",
        "rfphonqviwlz",
        "nezbwvufl"},

        new List<string> {
        "i",
        "i",
        "i",
        "i",
        "i"},

        new List<string> {
        "hvbatqdzu",
        "zvaqtumldb"},

        new List<string> {
        "hqxbn",
        "hqbn",
        "hqbn"},

        new List<string> {
        "ymikcgxdph",
        "kmduhyxqenpil",
        "kphygxmid",
        "hidfkzxypm"},

        new List<string> {
        "gve",
        "eygv"},

        new List<string> {
        "chds",
        "hsdc",
        "dcsh",
        "hcsd"},

        new List<string> {
        "pgakuncrze",
        "rpackxegnz"},

        new List<string> {
        "nhypde",
        "ncepyd",
        "ndpey",
        "ydenp",
        "yecndp"},

        new List<string> {
        "qgjpeu",
        "dfma",
        "uwnfvsja",
        "ohxcltz"},

        new List<string> {
        "emgqlacpfvjhux",
        "iudjrhwvfsxplmge"},

        new List<string> {
        "fmwbpeyxna",
        "wxdnafyeuibkj"},

        new List<string> {
        "rqhzeaojuigyvp",
        "ovtixs",
        "ixov"},

        new List<string> {
        "ms",
        "m"},

        new List<string> {
        "u",
        "v",
        "u",
        "u"},

        new List<string> {
        "yo",
        "yo",
        "yo"},

        new List<string> {
        "rhnmlxbvdteckzpsjaw",
        "cwzdraljthpnkxmeibvs",
        "wavdlhjezskmbrnpcxt",
        "pqadwkrmjbecxlnsthzv",
        "zkpbawrdhnclsmtexvj"},

        new List<string> {
        "djecv",
        "jewcd",
        "dejcb",
        "dvjce",
        "ecdj"},

        new List<string> {
        "adiotklhj",
        "oakyljdthi",
        "laticokjdh",
        "cialdtykjhfo",
        "kadoequjtlih"},

        new List<string> {
        "xcrfloupby",
        "lufciypr",
        "hrpcuyxfl"},

        new List<string> {
        "fwngabh",
        "njfgbwha",
        "hgafbwn",
        "bnhgawf",
        "fbamhngw"},

        new List<string> {
        "fwqe",
        "qfmvup",
        "fqa",
        "fqe"},

        new List<string> {
        "us",
        "us",
        "su",
        "su"},

        new List<string> {
        "lobh",
        "b",
        "bezjmi",
        "iblz",
        "sxbtav"},

        new List<string> {
        "tjkdlfwm",
        "klwmjfdt",
        "lcwfvjmdktq",
        "wlkfdtjm",
        "kmdwltfj"},

        new List<string> {
        "ngrzftbhoevylpiscd",
        "rhvsfbliwkndcaxzoejyp",
        "pfizbnldechsryov",
        "csnrdfibyvpolzeh"},

        new List<string> {
        "bhszfutjkrdxyeic",
        "iscrkhbjwdlyxzefp"},

        new List<string> {
        "rncodaythzpgelbu",
        "udcnathrbsogelzy",
        "yuhwglzreancitojbd",
        "yhelzuodacnrbtg"},

        new List<string> {
        "rv",
        "fwla"},

        new List<string> {
        "cuqsfzalw",
        "caqzwlfus",
        "wvcuflszaq",
        "saqlcuzfw",
        "uwscqaflz"},

        new List<string> {
        "hnp",
        "pnh"},

        new List<string> {
        "y",
        "y",
        "ergy",
        "y",
        "y"},

        new List<string> {
        "bmexpongud",
        "pgktrwhbneqaxov",
        "uiocxdgnebps"},

        new List<string> {
        "vxtckwno",
        "uenb"},

        new List<string> {
        "xgomqzsuivnab",
        "xusnvoqimgbza",
        "vqizsuxbnamgo",
        "zumvgxansqboi"},

        new List<string> {
        "s",
        "tsy",
        "lf",
        "rczxhvaopjew",
        "ltm"},

        new List<string> {
        "ipvefjzuhs",
        "hiecsvzu",
        "egykiushqbzdmovx",
        "izhvesau",
        "virsuhez"},

        new List<string> {
        "ld",
        "lg"},

        new List<string> {
        "xnvszluechwptgm",
        "uczsgowtpvlnmx",
        "zmwglrpscuvtxyn",
        "xmcstnvldzpguaw",
        "miwblxuzgcptjvksnq"},

        new List<string> {
        "ftwx",
        "zcritxpbo",
        "txsf"},

        new List<string> {
        "buzcyxlmoknsvqrdpwta",
        "iseplfgmrnjbwyvhtxk"},

        new List<string> {
        "cmjpkingdflra",
        "pcinrfladmkug",
        "icrmngljadkpf"},

        new List<string> {
        "saehfjb",
        "dtuheqwbgxyonksjlc",
        "ehvmjsb"},

        new List<string> {
        "fcty",
        "qopy",
        "hyfv"},

        new List<string> {
        "djkcm",
        "jklmbd",
        "mhrdkji"},

        new List<string> {
        "xaropbcylemuh",
        "benjalhducrpy",
        "ecypvsklubhtirwfa",
        "azrynhepulcb"},

        new List<string> {
        "xe",
        "xe",
        "xe",
        "ex"},

        new List<string> {
        "os",
        "os",
        "asob",
        "so"},

        new List<string> {
        "omtnjhdvcw",
        "ujotewxndvkfb"},

        new List<string> {
        "svqlbwrctd",
        "qfcbltsh"},

        new List<string> {
        "eof",
        "ebosn",
        "owfe",
        "xmyoe",
        "rvmoew"},

        new List<string> {
        "lmaus",
        "ulam",
        "auml"},

        new List<string> {
        "ky",
        "e",
        "f",
        "o",
        "e"},

        new List<string> {
        "figyhsul",
        "fguyhlsi",
        "lyhfigsu",
        "yuglfshi"},

        new List<string> {
        "myc",
        "cmyd",
        "myc",
        "mcy"},

        new List<string> {
        "dpnbhltm",
        "mp",
        "pmua",
        "opxm",
        "kvpgmuf"},

        new List<string> {
        "hk",
        "prv",
        "rh",
        "ixm"},

        new List<string> {
        "umlcksxonaqft",
        "bednjkpgirm"},

        new List<string> {
        "tj",
        "fxjtl",
        "tcmj",
        "tgzbvoprkqwu",
        "ist"},

        new List<string> {
        "bzats",
        "haosb",
        "abqsk",
        "abgs",
        "wvbfeuxasn"},

        new List<string> {
        "knwbzryjvde",
        "jdebkvryzcn",
        "djbzykenv",
        "euyfnkavdzbxmj"},

        new List<string> {
        "srgkpw",
        "apqbxusc",
        "tpmwq",
        "injolypfd"},

        new List<string> {
        "onxflkzsqwetmdrayupbijv",
        "blkorzdvyxiuawgnqpmstef",
        "fsnpxiueltmzovwrabyqdk",
        "bfxsyekrnvdqzoutwpiagmhl"},

        new List<string> {
        "jahltpfywcnobux",
        "hcwfadpkyzms",
        "oglqhpifavcwyx"},

        new List<string> {
        "migjhyvpnf",
        "ufjyqv"},

        new List<string> {
        "txoci",
        "z",
        "izvugb",
        "rpydhl",
        "xbm"},

        new List<string> {
        "ajlfb",
        "bfl",
        "jslof",
        "fzrnmlqek",
        "ofdl"},

        new List<string> {
        "crbuzmjlnsoixgyatf",
        "gkbasourfjcimlxtnv",
        "ndmwxstgfoubrcljia",
        "qmfoxlhnrubetgjsaci"},

        new List<string> {
        "msr",
        "ir"},

        new List<string> {
        "ypv",
        "vp",
        "vp",
        "rpv",
        "pv"},

        new List<string> {
        "jquxr",
        "hngqptklaeuimrw",
        "frbdvoucq",
        "drusqy",
        "xqdzur"},

        new List<string> {
        "ifnuymdhesgzpjkroqwc",
        "nqshkiwoyfcupmjgrezd",
        "cpqmjwreogdyhuznfkis",
        "pzijugdfykreocnqhswm"},

        new List<string> {
        "wletxi",
        "twlxe",
        "tjewrlmx",
        "sflciwxet"},

        new List<string> {
        "cqmtspovlhxzwknijgdbya",
        "jktgvsnfmobiwxplqzyc",
        "oniglefctyxvbzpsqkmjw"},

        new List<string> {
        "aqbujkmecp",
        "jcakubpeqm"},

        new List<string> {
        "ms",
        "s",
        "g",
        "d",
        "s"},

        new List<string> {
        "ymb",
        "bmy",
        "tbym",
        "zmby",
        "zymb"},

        new List<string> {
        "zmajf",
        "afd"},

        new List<string> {
        "saqutcgmzpvrx",
        "qzumvpcraxgts",
        "zcmgpquxnrsavt",
        "uxztgakmcqsypvr",
        "xmvqurazcptsg"},

        new List<string> {
        "xgcnvwikzmh",
        "jualdmfogexcp",
        "vrixcgqmbt",
        "gcxms"},

        new List<string> {
        "hlf",
        "jzeubrawvgonf",
        "yqxcpkhf"},

        new List<string> {
        "wpkbqaver",
        "kwenvqrpbd",
        "pswekvrqb",
        "vbepwrqk",
        "rkweqpvb"},

        new List<string> {
        "r",
        "ef",
        "eqh",
        "e"},

        new List<string> {
        "nvzfi",
        "znmev",
        "ohnlvzk",
        "vnezjc"},

        new List<string> {
        "whkjfmbtausedyvqx",
        "tmqxhkfsydevuwba",
        "srvfbdkymwtqxiehcupa",
        "lsatxumedbwfkvyhq"},

        new List<string> {
        "aligpe",
        "glpaie",
        "aliepg",
        "eilagp"},

        new List<string> {
        "xvigpscledkfwjbztn",
        "sgejnlvxdtbzfckpiwh",
        "vlcwftebgjsndpzixk",
        "swkzngbdcexifltvjp"},

        new List<string> {
        "zrethjknvsdwf",
        "qldrsgybijfhz"},

        new List<string> {
        "tlhogxqens",
        "habqtneudxljgks",
        "xqtsnelhmgo",
        "qlnteghxs"},

        new List<string> {
        "bhc",
        "ec",
        "ch",
        "jgiyc"},

        new List<string> {
        "xzfioelc",
        "ozilcexf",
        "zfeoxcli",
        "clefxioz"},

        new List<string> {
        "uqjkybfsehwxzlptrni",
        "caryzpjnukwlxesgtbhf",
        "jzwkubtlsypqhneforx",
        "xlrwkspjyufnzebht"},

        new List<string> {
        "tvzuoab",
        "bv",
        "bgv",
        "bv"},

        new List<string> {
        "ltujriwpmyna",
        "jtrmpw",
        "mwtspjr",
        "tmorwjp"},

        new List<string> {
        "tyjsqfripxz",
        "rpzxytjqsif",
        "xtyrspijzfq",
        "ctixjyfzrsqp"},

        new List<string> {
        "ycjihklnd",
        "ezvuqn"},

        new List<string> {
        "chtgvuilo",
        "uvgticloh"},

        new List<string> {
        "hmgyx",
        "yghlx"},

        new List<string> {
        "oejuvpaigsrqh",
        "pijsveugqroha",
        "eviqopasurjgh",
        "iroqaphsuevjg",
        "qauzoirjghsevp"},

        new List<string> {
        "gumpokcdtinjberaxqfz",
        "xdounkjezmrpfbicqgt",
        "pzfgicekuymqnrxodbjt"},

        new List<string> {
        "npy",
        "nyp",
        "ypn",
        "ynp",
        "ypn"},

        new List<string> {
        "xfhucsdlpveoir",
        "mnzobifxtkr"},

        new List<string> {
        "hy",
        "yh"},

        new List<string> {
        "iumnsyqgw",
        "wgyismd",
        "rdmgwiyhfs",
        "xzgywmis"},

        new List<string> {
        "dwfplygjx",
        "vuhewxszq",
        "bxnrmocki",
        "xtyea"},

        new List<string> {
        "ouyqrtczxwj",
        "xqtyujwocrzb",
        "qjxuyrotkwcz"},

        new List<string> {
        "qjtcyaoexnvl",
        "nxtaqkjloce",
        "tzcongqajpxle"},

        new List<string> {
        "olubpxmfdayik",
        "leqomjitfbay"},

        new List<string> {
        "lygezhjmbi",
        "clvphtkud"},

        new List<string> {
        "vp",
        "pv",
        "pvkfw",
        "pmv"},

        new List<string> {
        "rpsclgeuhmkja",
        "mcgypeuswakhio"},

        new List<string> {
        "ajdpyugzbcswtei",
        "pcadiemunsbgw",
        "ludvapgecqsfbwi",
        "elsugdcfnhbipwa"},

        new List<string> {
        "vfzoqrbgjycuenwlxdts",
        "fgdnclwtoyxqvuesjrzb",
        "ejatdrohipbxucyvnwzfsqgl",
        "utsloejxznvwybdrfgcq"},

        new List<string> {
        "vwkaomqupbitzrh",
        "pvawbkoztumqhr"},

        new List<string> {
        "ijbcxau",
        "cijuoea",
        "cjifuyqa",
        "cubtxaji",
        "jxcaiu"},

        new List<string> {
        "zsankwocrhb",
        "dchzrbkmaswon"},

        new List<string> {
        "jdcfne",
        "nfedjc",
        "qeczfjn",
        "fcejqn",
        "jnefvc"},

        new List<string> {
        "vpzmjnb",
        "zbvjqdys",
        "zbxpdjmial",
        "ktouerbjgwz",
        "jsbzhc"},

        new List<string> {
        "zbsi",
        "zisb"},

        new List<string> {
        "txvngrdas",
        "vadsrgtx"},

        new List<string> {
        "qgnvriol"},

        new List<string> {
        "a",
        "a",
        "a",
        "f"},

        new List<string> {
        "mvuywsiaqcl",
        "nqavfwhsicz"},

        new List<string> {
        "yuomdcgnvwhakel",
        "auwkhgoycmlevn"},

        new List<string> {
        "icagyksq",
        "crsahngi"},

        new List<string> {
        "vb",
        "ftlgxroupjyis",
        "cvh"},

        new List<string> {
        "vcimkpuafwqbryogjxdt",
        "mxyftdriubhawgjcvpkq",
        "rybptdfhaoxkgvmjuciqw",
        "idxrkpygcwvltaefmbqju",
        "trmjbiqpnyhvscwxafukgd"},

        new List<string> {
        "esbdajqhvl",
        "jdqlszev",
        "foedswyvlunq",
        "ixevsdtblmq",
        "rvetlqsdk"},

        new List<string> {
        "islfcy",
        "yif",
        "bify",
        "ioymf"},

        new List<string> {
        "hrcqst",
        "pecr"},

        new List<string> {
        "gwd",
        "wdg",
        "dgw",
        "dgw"},

        new List<string> {
        "ksyqndmahzgxejfp",
        "sexhgkfzdapyqmjn",
        "dejzykngpamhfqxs"},

        new List<string> {
        "fmohepc",
        "pucg",
        "cp"},

        new List<string> {
        "xvdmorgbsnawjlecztykpif",
        "rsmwdvfplkencbzaotgjix"},

        new List<string> {
        "wjhkg",
        "gbkh",
        "dklegsrmhy",
        "hukgw"},

        new List<string> {
        "flwu",
        "lnzckx",
        "vetd",
        "myj",
        "xwcp"},

        new List<string> {
        "vjhnftpyr",
        "nfhjvyrpt"},

        new List<string> {
        "lne",
        "nel",
        "nle"},

        new List<string> {
        "hgqjcse",
        "chfja",
        "jqcus",
        "kozlctmiy"},

        new List<string> {
        "h",
        "h",
        "h",
        "h",
        "h"},

        new List<string> {
        "lwco",
        "wloc",
        "tlkuwco",
        "lwoc"},

        new List<string> {
        "egzxalry",
        "zyxaklpge",
        "zlwergaxy"},

        new List<string> {
        "nghrpie",
        "nqz",
        "au",
        "ytcdsfw",
        "lbj"},

        new List<string> {
        "jkcetobxnsyqwfdzuglv",
        "gsnwvmzjxiteryacp",
        "njcshegytvzxw"},

        new List<string> {
        "qtsf",
        "st",
        "ts",
        "ts",
        "tps"},

        new List<string> {
        "ckmnriay",
        "sytrcng",
        "ncywrg",
        "ncygr"},

        new List<string> {
        "rcnyivhla",
        "ebgfdotjuks"},

        new List<string> {
        "tfjcihobxywuvpezarkg",
        "hckgoruvbxzenjipaywft",
        "fwhtyrvebajgpkcuxoiz",
        "oaruytpwzikbfhjxevgc"},

        new List<string> {
        "oaxzdr",
        "dopg",
        "opzak",
        "woqg",
        "notveuy"},

        new List<string> {
        "l",
        "l",
        "l",
        "qn",
        "m"},

        new List<string> {
        "dvjuhlfmectponaxwyib",
        "yfaltvohpnquirmzjdcxsb"},

        new List<string> {
        "ib",
        "bil",
        "ib",
        "bi"},

        new List<string> {
        "sd",
        "lo"},

        new List<string> {
        "s",
        "wsvef",
        "va",
        "jwvf",
        "rdho"},

        new List<string> {
        "or",
        "lor",
        "kro"},

        new List<string> {
        "ze",
        "e",
        "e"},

        new List<string> {
        "teh",
        "ef",
        "ek",
        "fe"},

        new List<string> {
        "dfbzgqpsk",
        "pzgfaydsuxq",
        "pgfqdsz",
        "kzrfgqsdwp"},

        new List<string> {
        "rxqgndcawfb",
        "loyuzbmejivkhs"},

        new List<string> {
        "jilybertvwkqcma",
        "nuelmho",
        "ledhomnuz",
        "esfml",
        "gxzpelm"},

        new List<string> {
        "mikrjtbophlqduwsexvcy",
        "mbrqlycjvskdwithuopex",
        "qlwrcduykbmxsovijthep",
        "xevmqplwtjyckbouirhds",
        "bmytdrsxvupciloewqjkh"},

        new List<string> {
        "wrnkojbmtzvchulxaisepyfdq",
        "xicryqvwkumtozjdhsflegpabn"},

        new List<string> {
        "yjxpwakhd",
        "klhj",
        "kjih",
        "nshrgujk",
        "skjhm"},

        new List<string> {
        "fasz",
        "aef",
        "gmqipcfanvt",
        "hsafl"},

        new List<string> {
        "m",
        "m",
        "m",
        "m"},

        new List<string> {
        "ksyargzucfq",
        "lyvjrmapbhqwt"},

        new List<string> {
        "b",
        "b",
        "by",
        "b"},

        new List<string> {
        "d",
        "dx"},

        new List<string> {
        "lqdmrovwifbhpkg",
        "dqvofhgzwipkmlsrbn",
        "ihmvkdqbroflgpw",
        "bghqvfipwodlkrm"},

        new List<string> {
        "qzuabnldjsf",
        "nabfzldjq"},

        new List<string> {
        "ihkpj",
        "skhj",
        "khj"},

        new List<string> {
        "pfhzgidaoumyclqwjb",
        "oubaqdmhlzgjfcwyip",
        "maqwihopcbdlzyfujg",
        "aydlupjizwhofcbgmq"},

        new List<string> {
        "dcifuhzpj",
        "udizhcfj",
        "uzcrijdhmf",
        "uhfdcjize",
        "dxbjhzaifuc"},

        new List<string> {
        "mijthslvq",
        "vslmjqhi"},

        new List<string> {
        "yilmjrbsaugeqv",
        "sokhewyczmgufabjv",
        "maxbtsguvyejdlp"},

        new List<string> {
        "bdysxeouwcn",
        "otszxpnm",
        "wbsxonkr"},

        new List<string> {
        "btwjys",
        "bjy",
        "jby",
        "bjy"},

        new List<string> {
        "elobcuyqvps",
        "ubeloqvyspc",
        "elvcypobusq",
        "qbcsuyeplov"},

        new List<string> {
        "zshbqdmcxntwokevlpjugr",
        "lubzdvwrksnhqcptmgyexjo"},

        new List<string> {
        "csmyezwqxdpfh",
        "zexmdwhcqsyfp",
        "fdqeysphczxwm"},

        new List<string> {
        "wx",
        "x",
        "x"},

        new List<string> {
        "hmudcwtoix",
        "duitheoxcmw",
        "xeomwhtiucd",
        "xmthuoiwdfc"},

        new List<string> {
        "qgh",
        "xburedysok",
        "izaqc",
        "vac"},

        new List<string> {
        "c",
        "xd",
        "y"},

        new List<string> {
        "bcqxzv",
        "czxvb",
        "xcdv"},

        new List<string> {
        "rstdhfvyupaqnilgkx",
        "thmdrvqkgyxpcewlafu",
        "xkvfgpdtahinlsyquzr"},

        new List<string> {
        "ionzfdqwacl",
        "aliwfzcnodq",
        "ysdoijcnpqalfwuzr",
        "dialwcqofezn",
        "lqwfncaoeizd"},

        new List<string> {
        "vjnzhsyokprdwqt",
        "slufncexiadgw"},

        new List<string> {
        "lmanxbzrvkgju",
        "cotlneyfp"},

        new List<string> {
        "ehcxwrapivns",
        "nvewjcaspixr",
        "ecpdkqxvastmn"},

        new List<string> {
        "okgbcwvytiznshexu",
        "kwyszldpbuthoxnj",
        "kotjwysnuzxhb",
        "juzbwshqatknxomy",
        "krzthuxqynfoswb"},

        new List<string> {
        "rjfbkgapzqiwnecoh",
        "ohjnfzrabwcgqipek",
        "bgmzawokijpnqhfcre",
        "vwherzicopkqbanjufg"},

        new List<string> {
        "qrwxpijsu",
        "apuwbfirqjs"},

        new List<string> {
        "rgynxak",
        "uaxpirf"},

        new List<string> {
        "sf",
        "f",
        "uf",
        "ihxfzdy"},

        new List<string> {
        "dvcrwsbpziytjqhnuoa",
        "ehgudqsyzkbrnpt",
        "sptdxznyeuhrqb",
        "uqzbhfynsrtdp",
        "srpymhztqbdunl"},

        new List<string> {
        "rqly",
        "dlaxpwrji",
        "rbljwh"},

        new List<string> {
        "grascdfzlpxoeqvtyn",
        "xcnfwvryspeukbodzlatg"},

        new List<string> {
        "oqlucpvdizwh",
        "trakqib",
        "agitjq"},

        new List<string> {
        "yslaopvredmzcjnighqtfk",
        "ohgqmkatyrledjnifspzc"},

        new List<string> {
        "xwliz",
        "czvfiqxw",
        "gyswmhb"},

        new List<string> {
        "fpndrua",
        "aurfp",
        "rpuaf",
        "raufp"},

        new List<string> {
        "eukjozdbqcxpyl",
        "qyfmkxzboulcpejd",
        "zlquxdybcepjko"},

        new List<string> {
        "dlgxuchpnmosfr",
        "dxcoshzfguykpvm",
        "awgdpubxiefsohqj"},

        new List<string> {
        "abop",
        "abvpo",
        "napobc",
        "vapob"},

        new List<string> {
        "wryqzk",
        "kwqyc",
        "wyzqkr",
        "ykwqzo",
        "ywkqr"},

        new List<string> {
        "jbk",
        "tkbj",
        "bjkcn",
        "bjk",
        "jkb"},

        new List<string> {
        "fagxpiolshctke",
        "xtiochpabflsk"},

        new List<string> {
        "fqdteprviumho",
        "meitwhforpduqv",
        "utfrhempwodviq",
        "nehmyipftrqdlucov"},

        new List<string> {
        "anqedwylpuv",
        "edoqpufvwzx",
        "tiuqvjhcrpewkmd",
        "psvudgeqw",
        "gdwulbpvqze"},

        new List<string> {
        "znugjimbptvefqhxr",
        "suxktycoplwe"},

        new List<string> {
        "qtludyrb",
        "yqrludv",
        "lqdyur",
        "ldqryu",
        "lurkzqyvad"},

        new List<string> {
        "cwmikuhtgole",
        "tzghimclkue",
        "igtxkcpsmlydhe",
        "khtglcqmuie",
        "imhwecgltk"},

        new List<string> {
        "pschkw",
        "schnwpultqk",
        "iwzohyej"},

        new List<string> {
        "dwxbfsq",
        "dswfbqx"},

        new List<string> {
        "tpby",
        "ptq",
        "qpt",
        "tp"},

        new List<string> {
        "nqtocmfdjpskriev",
        "agvctwbfxkyj"},

        new List<string> {
        "kqdewilc",
        "qweluikd"},

        new List<string> {
        "qbyv",
        "vb"},

        new List<string> {
        "k",
        "jmp"},

        new List<string> {
        "lvnqfrdctwzk",
        "xtljgevz",
        "tusihlzv"},

        new List<string> {
        "vrtojs",
        "rvtzosj",
        "jrcqvsutohwl",
        "zvpsjtgoar"},

        new List<string> {
        "wkh",
        "wh",
        "hbtl",
        "qh"},

        new List<string> {
        "jzcdykoafitepbxlqvmns",
        "yqcpzvksoinjlebfdatxm"},

        new List<string> {
        "rvmjscznedib",
        "smineqdwcjvrz",
        "duejsmrncviz",
        "idrmvcsezlnjf"},

        new List<string> {
        "qognsitfwkcmvd",
        "qbonaiuktrc",
        "ctkiyqnou",
        "iptrcqjkhzoaxn"},

        new List<string> {
        "msjqvb",
        "wxfpej"},

        new List<string> {
        "zgdmaphfuiols",
        "oemgjuxavhcsfzdilp",
        "gopdafmiulshz",
        "zaphsolgumftid",
        "fdgmahupzsoil"},

        new List<string> {
        "svdmfou",
        "uofmsdv",
        "fuvsodm",
        "uvmodfs",
        "vsoumdf"},

        new List<string> {
        "pn",
        "gn",
        "np",
        "hsnc",
        "nem"},

        new List<string> {
        "ondycsxijkhztmb",
        "sdtmkibxjyu"},

        new List<string> {
        "cfdbyz",
        "dbcfyz",
        "ocfbzyd",
        "bfzdyc",
        "fcbydz"},

        new List<string> {
        "rycjvanzo",
        "qzecro",
        "zrqoc"},

        new List<string> {
        "iytqmcheg",
        "gyiectmh"},

        new List<string> {
        "idcpxfhmwbutsogkazl",
        "iwoydxlchsezubtkgpma",
        "fcwtdoulsphxmibkazg",
        "woshuclmdzibaxpgkt"},

        new List<string> {
        "lvpfdtgw",
        "jysqnxkozermwc",
        "aywockbuh"},

        new List<string> {
        "hlgrnxofckptmqvaebsdwi",
        "lhqtonswkdmvrcibfxape",
        "wqimranhsepdotcfbxlvk",
        "ksxwcidfaoqlbrvenhtpm",
        "dncheprotawksmxflvbiq"},

        new List<string> {
        "mkcyzpfseqg",
        "yqgmkczvt",
        "zbiqoynxkrjd"},

        new List<string> {
        "qalxdcz",
        "wuchrlszbepm"},

        new List<string> {
        "tisl",
        "tjoycs"},

        new List<string> {
        "uixno",
        "zicsuorn",
        "xuqiokn"},

        new List<string> {
        "qadwnkvfzltpib",
        "ctehovpradslgxujwnmbfiq"},

        new List<string> {
        "lnbzhdvqucg",
        "udvzgqlcbhn",
        "zcvhlqndgub"},

        new List<string> {
        "dnkeso",
        "enskdo",
        "sneokd",
        "oknsde",
        "dekson"},

        new List<string> {
        "aqxyozpubvjgcr",
        "ybpjqrcoagxuzv",
        "yrajbvpogcuqzx",
        "ubarxgvqyjpzoc",
        "xqupgyrovzcjba"},

        new List<string> {
        "galcpxukjvdfz",
        "jcvgulafdxpzk",
        "gchxzuajvlkpdf"},

        new List<string> {
        "pds",
        "euwsvd",
        "dsf"},

        new List<string> {
        "phvzus",
        "rhlztp",
        "phzdla"},

        new List<string> {
        "rfpbcyd",
        "rbdycpf"},

        new List<string> {
        "brwpvt",
        "spvtrjb",
        "vytpbr",
        "tbsrvwp",
        "rvtpb"},

        new List<string> {
        "efmkb",
        "bemkf",
        "bekmf",
        "temfkb"},

        new List<string> {
        "jyzinpl",
        "efxqapwo",
        "rzup",
        "dpcr"},

        new List<string> {
        "bawpqhnvmtuxsflizck",
        "avqcthmkpifswzlnxu",
        "slmqcpnxivzahkutwf"},

        new List<string> {
        "zsakf",
        "skfeza"},

        new List<string> {
        "neqbzcajpw",
        "onzqsaxhcbwljdm"},

        new List<string> {
        "do",
        "do",
        "od",
        "od"},

        new List<string> {
        "rieubqwd",
        "cqrupedvitb",
        "edbqirun"},

        new List<string> {
        "hvrpjfwud",
        "zpauyi",
        "kbxmeupq",
        "uptwfhgzaj"},

        new List<string> {
        "vkqf",
        "fhnpyx",
        "gawucsozmdi",
        "tbrpnlekj"},

        new List<string> {
        "neriadovyhuzkqgc",
        "rqflhpywekobzstjumcn"},

        new List<string> {
        "bvzumtnorcqhyilgwe",
        "tvnioyzlwmerhgbuq",
        "wylinhomzvetrbuqgdc",
        "ghbltunyeopfrqizmwavx"},

        new List<string> {
        "jvrieqgfklch",
        "pjndlghruvif"},

        new List<string> {
        "xbwvzikjtnhfayp",
        "tgcadrqjxuel"},

        new List<string> {
        "lbxsjvamqwdfutrck",
        "bwrxdaqzuftlmvk",
        "zbtmakqxdfpruwlv"},

        new List<string> {
        "jrisgax",
        "gries",
        "rigts",
        "girjspe",
        "pasieghr"},

        new List<string> {
        "reqwbsjzlghkv",
        "gkmvstdcaroqbzjp",
        "qhrvngfjzsblku"},

        new List<string> {
        "vtilu",
        "i",
        "i"},

        new List<string> {
        "cmizk",
        "kmcdwi",
        "tokljcmiv"},

        new List<string> {
        "gcmxdqoysrjhzvunkbtpwfi",
        "rsvhxmjgbdupqotnywkcifz",
        "byczhwkrnmujqoidfpstxgv",
        "romdtyjgwbcpihafslznxqkvu"},

        new List<string> {
        "kcx",
        "wfsoxa",
        "rcxk",
        "xecl"},

        new List<string> {
        "xhljmt",
        "lheratmxj",
        "fhmxtcwlb",
        "xhlmaritg",
        "thxmalo"},

        new List<string> {
        "usfhzlidpmxvonr",
        "luvxhfdcmrpn",
        "xpfldmuhvrn",
        "lumvrnedxfphk",
        "lpvdnfurmxhk"},

        new List<string> {
        "ukqco",
        "bg",
        "d",
        "x"},

        new List<string> {
        "wshkqpojltbyexrvfcmaiugd",
        "ptxsveguqfjdimboarwclhky",
        "figabshrwotdqjepmlxkyvcu",
        "imgseacrdxfkyptwhvoblujq",
        "wflyahpcgbrsmvkitejxoqud"},

        new List<string> {
        "cdfihsmoy",
        "smieho",
        "mhsoi",
        "oenshami",
        "ozlhswmi"},

        new List<string> {
        "pefjcrt",
        "vdfzpwrtc",
        "ptegcbfr"},

        new List<string> {
        "uet",
        "uet",
        "eut"},

        new List<string> {
        "ubyk",
        "ukygtrole",
        "jukzxqhvy",
        "eumldfkyos",
        "knaiuywrm"},

        new List<string> {
        "ncjzgqof",
        "qafcjnzog",
        "zojngqfc"},

        new List<string> {
        "uxkryjwflco",
        "psolcaquvwkdnizre"},

        new List<string> {
        "linoudwypsgbvmx",
        "wugpsidobmnryv",
        "udynmoswgb",
        "kydhcbnozusmegwt",
        "sygbrmudwnafo"},

        new List<string> {
        "sfkatdxbyqezirv",
        "skvtiarxeybzf",
        "viyzrlampbxtskfe",
        "vyqazixrftsebk"},

        new List<string> {
        "bjwqhkryuz",
        "zmwkyuqhjrb",
        "qjuwrzhybsk",
        "hyzwujqbkr"},

        new List<string> {
        "wjahb",
        "jywahb",
        "jbawh",
        "jwhba"},

        new List<string> {
        "aetucrzpvn",
        "esznbtkoy",
        "eztdn",
        "zonte"},

        new List<string> {
        "dkpu",
        "mpqcixu",
        "pwu",
        "psu",
        "pudkhyv"},

        new List<string> {
        "ydtaink",
        "atndubyk",
        "ocytkdain",
        "wnvgzytadkps"},

        new List<string> {
        "mtne",
        "mten",
        "netm",
        "netm"},

        new List<string> {
        "l",
        "lz",
        "l",
        "l",
        "l"},

        new List<string> {
        "wfu",
        "wfu",
        "uwf",
        "uwf"},

        new List<string> {
        "wcxsiaokerlumjnqyfvhtbgd",
        "abrlxsfzqjuwnotehgmdkcyv"},

        new List<string> {
        "kate",
        "aekt",
        "akte",
        "teak"},

        new List<string> {
        "spxqizdhbuokw",
        "pwdxibkuzsqohr",
        "qkszwvaulbpdthoig",
        "ouwqdkzypbhis"},

        new List<string> {
        "xlskunagot",
        "onxagbulsem",
        "olnsagux"},

        new List<string> {
        "mhrst",
        "hm",
        "hm",
        "hknuimeg"},

        new List<string> {
        "pzwtsrfhcmqidnaeybgxkluvoj",
        "keugsjaimzwbxqcvndytfhlpor"},

        new List<string> {
        "gezfhs",
        "nsyftgje",
        "csifykhegj",
        "fdrgmqvse"},

        new List<string> {
        "eqagibxdlst",
        "gdisebqlxat",
        "eogtidqaslbx",
        "tdbqslaigxe",
        "eltabiqxsdg"},

        new List<string> {
        "hopcmlyfka",
        "zleytsfdpcgbn"},

        new List<string> {
        "iq",
        "qm",
        "ajngr"},

        new List<string> {
        "jgnh",
        "pdqzyblwuh",
        "tovghj"},

        new List<string> {
        "jsztoebhflkrncuygi",
        "epzghnfcoljutisryk",
        "ugnysrzftjieholckm",
        "kyeuglztrshjnbfico"},

        new List<string> {
        "q",
        "q",
        "q",
        "q"},

        new List<string> {
        "zch",
        "zhc",
        "ch",
        "cdgh",
        "hcw"},

        new List<string> {
        "nubdmx",
        "xunbd",
        "dxunb",
        "wbxund",
        "udnbex"},

        new List<string> {
        "dfcauy",
        "afdcuy",
        "dfcuya",
        "uafcdyn",
        "fudyac"},

        new List<string> {
        "cdgkzushqaevntrop",
        "ndhzqxgksrpotcaev",
        "qetkhzodnsrcgvap",
        "epcvrnokadgzhstq",
        "nozqsrhcegpavdkt"},

        new List<string> {
        "wbpaec",
        "jmfyir"},

        new List<string> {
        "wozuxcnegrjikhy",
        "eritoghwxluynkjzc",
        "okrecihjuwxznbgy",
        "nuwirjxkezyghoc"},

        new List<string> {
        "g",
        "q",
        "r"},

        new List<string> {
        "jdr",
        "rj",
        "rj"},

        new List<string> {
        "ogs",
        "szgo",
        "osyugw",
        "sogj",
        "ogs"},

        new List<string> {
        "ixnlzuvyar",
        "ryznvumlqfexi",
        "icylvxzrnu",
        "lurxhyvzni",
        "nixylzvrut"},

        new List<string> {
        "xv",
        "xs",
        "x",
        "kfwax",
        "hxu"},

        new List<string> {
        "dkjohmvtxscaqpyzbie",
        "bdvtjzyhoaecmxpiqks",
        "eyhsxtdoazqvcbpmikj",
        "oevbpqstmxchykizdja",
        "mbvjkyzihcsqepadxot"},

        new List<string> {
        "vkqgipbhsmczdaery",
        "ckomvhixysdq",
        "cvixystlohmkqd",
        "noyqmhdfuvkcis"},

        new List<string> {
        "jeqcx",
        "xjqec",
        "jceqx",
        "qcjex"},

        new List<string> {
        "okncyha",
        "kha",
        "wdqkamrhi"},

        new List<string> {
        "uajtdcwglpxf",
        "ldxgwjauscp",
        "avciwljdyxupg",
        "lgctupaxwjd",
        "zjblqcxwkrgdanup"},

        new List<string> {
        "ogpcjvltkizyrubnshf",
        "xrpukdzmcsqlnofejigtvbh",
        "nhvztyfbsuicjlrkopg"},

        new List<string> {
        "rklv",
        "lnvr",
        "vrsle"},

        new List<string> {
        "tdoeb",
        "toebz",
        "jbot"},

        new List<string> {
        "ahntqkrwvxc",
        "htray",
        "tehar"},

        new List<string> {
        "xbis",
        "q",
        "p"},

        new List<string> {
        "y",
        "y",
        "y",
        "y"},

        new List<string> {
        "svpnoi",
        "vsnroip"},

        new List<string> {
        "rkdetpah",
        "hpdmjrekao",
        "ybzcrhkguew"},

        new List<string> {
        "ha",
        "qkj",
        "vzcjq"},

        new List<string> {
        "ubjqtpfxalnv",
        "uftvrxjqpbnla",
        "tbunjpvqfxalh"},

        new List<string> {
        "izesvwkmyohlqr",
        "jhtqnmyiawpskz"},

        new List<string> {
        "jdoqhwbftxpavygze",
        "zydthexqgwpkaovnfj",
        "cdsvzfxwtigjamryhqep"},

        new List<string> {
        "srvdcubotnwkx",
        "dmhpqyzikcslargnj"},

        new List<string> {
        "tcmoi",
        "ptqeci",
        "buvtic"},

        new List<string> {
        "kisteoqyjapdrcgfx",
        "sqdxjckrfoytap",
        "ztrdcxsayfkqjop",
        "rjxfkpdaqysozctm",
        "fyctdxsrkaojqpu"},

        new List<string> {
        "chtzwxr",
        "ozcrsthux"},

        new List<string> {
        "oqahrvuinct",
        "uavtnforke",
        "ounartv",
        "akunvrot",
        "uonvftmar"},

        new List<string> {
        "hveqfbrwkdosnay",
        "zofutwkxmpgnljic"},

        new List<string> {
        "sexkhbfwvagzjq",
        "hryamtnf",
        "phacfo"},

        new List<string> {
        "yzdbglajf",
        "lybfajgzid",
        "ldjagfbyz",
        "djlyzfbga",
        "lgyjzfabd"},

        new List<string> {
        "mk",
        "km",
        "mk",
        "mk"},

        new List<string> {
        "vupse",
        "utvf",
        "veu",
        "noubvi",
        "vslpu"},

        new List<string> {
        "gxcz",
        "xgcz",
        "zxgc",
        "cgxz"},

        new List<string> {
        "xsgkotfqldj",
        "ltwmgjofxdsk"},

        new List<string> {
        "sptmvzkdoxwqrefgn",
        "zwa",
        "ybwjzuchl"},

        new List<string> {
        "jqdxzbhsctn",
        "ocsat",
        "tokewcs",
        "usktc"},

        new List<string> {
        "hqctliu",
        "ilchptqu",
        "quchitlj",
        "lcuqtih",
        "ipbqctulh"},

        new List<string> {
        "wgihvaqrxpczsmflydjuket",
        "mclinzpejdgtsfwqhyxkrvua"},

        new List<string> {
        "sdgycjfmukezpx",
        "bvwzqrmnti"},

        new List<string> {
        "xoizphtv",
        "mfzxhitopvag",
        "hzipoxtv",
        "rdtxiopvhz",
        "hnvopxzti"},

        new List<string> {
        "nluidsozya",
        "sxcqziol"},

        new List<string> {
        "vf",
        "vf",
        "fv",
        "fv",
        "vf"},

        new List<string> {
        "x",
        "x",
        "mdtxr"},

        new List<string> {
        "cspzldibokwq",
        "bnktcpxzwlmeijqvd",
        "pqkcyzhwbild"},

        new List<string> {
        "zmdrvaxgtiq",
        "jydtqgcm",
        "dqgtxlm",
        "fnxtegsmqrad",
        "gkqmdtv"},

        new List<string> {
        "zwulxhevg",
        "jtwpxvdczlsh",
        "znxlvhw",
        "laozqhwrvgx",
        "xmlvhfizw"},

        new List<string> {
        "ixjseyvhfcupkz",
        "jifehpsczxyvu",
        "ixhvyweujozsmpfc",
        "wpfskxljyceiuzhv",
        "gydqfcphztjesirxuv"},

        new List<string> {
        "bdolgwcua",
        "cogbawu",
        "ugabwco",
        "obtcwagu",
        "wcbuoga"},

        new List<string> {
        "txjldzfwhbqcspemvgkiyn",
        "kmdswhijztqfyclebvnpgx",
        "szydcbewfnmvpxhijtqklg"},

        new List<string> {
        "jfxehga",
        "aoxhj",
        "vuaozhjqxi",
        "hxjqvia"},

        new List<string> {
        "cyknepoazhsml",
        "dhoecksznaylp",
        "uktqnwrhivasbcxlepy",
        "yshcfelankp"},

        new List<string> {
        "mgbwxcvkrl",
        "tzoauif",
        "qpjntdoysueh"},

        new List<string> {
        "n",
        "n"},

        new List<string> {
        "briatspxkzdwue",
        "abdgifsljzuo"},

        new List<string> {
        "vklpca",
        "lkavpc"},

        new List<string> {
        "lfxguyd",
        "dzfuex"},

        new List<string> {
        "zqxod",
        "qxd",
        "qdx",
        "xhqude"},

        new List<string> {
        "ovigcernldjwzy",
        "idegzjvrwlyn"},

        new List<string> {
        "cb",
        "bc",
        "rbcxdh",
        "bc"},

        new List<string> {
        "givtqeysrcjfzumod",
        "isrgtyjavfwcoekdzmuq",
        "zdgtmescqrvyiujfo",
        "ogumbvzcdejyrtsiqf",
        "qdfjsoetuvrymzcig"},

        new List<string> {
        "fwruktphdzan",
        "zkfpnrthwaud",
        "unrhpzwtdafk"},

        new List<string> {
        "odlubiyrqzfevnjpakwtgmx",
        "lrsdjkauwbvztfymqginoxpe",
        "omeqnvadjubzwkxritgyplf",
        "fjzywpmkeaotldvguqbnixr"},

        new List<string> {
        "emayzjxi",
        "axzreyckm"},

        new List<string> {
        "lduhiqwpjrnkto",
        "ntcrjqdkwphloi",
        "thjdwnklqirpo",
        "tqpdwjilhoknr"},

        new List<string> {
        "vi",
        "iv",
        "vir"},

        new List<string> {
        "xoetdfmrbvl",
        "dfebtomilx",
        "fbltmdexo"},

        new List<string> {
        "hmcuklq",
        "lusqkch",
        "iqjpulckh",
        "suchkql"},

        new List<string> {
        "gu",
        "gu",
        "gu",
        "ugn",
        "gu"},

        new List<string> {
        "lvtfiecgrzxhm",
        "tircajhne"},

        new List<string> {
        "fm",
        "if"},

        new List<string> {
        "subio",
        "buosi",
        "boisu",
        "busoi"},

        new List<string> {
        "uwkhxabg",
        "hofrviyqz",
        "wkuhs"},

        new List<string> {
        "zoswictnpdvjegqymbfura",
        "cmrnqyupavzbfjegidtso",
        "xpycanfhjrulqzvmtdbsegoi",
        "iqdrfstyjgabzenpvckomu"},

        new List<string> {
        "kzardg",
        "dkzgura",
        "zdagrk",
        "gdrakz"}
    };
}