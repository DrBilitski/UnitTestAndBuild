namespace TestProject;

using FileManager;

public class Tests
{

    
    [SetUp]
    public void Setup()
    {
      //  FileManager fm = new FileManager();
       // fm.DeleteFile();

    }

    [Test]
    public void TestAddRecToEmptyFileThatExists()
    {


        // delete all contents of the file
        using (StreamWriter writer = new StreamWriter(FileManager.filePath, false))
        {
            // Writing nothing effectively deletes the file's content
            writer.Write(string.Empty);
        }

        // add to empty file
        FileManager fm = new FileManager();
        bool result = fm.AddRecord("abc,Jim,B");
        Assert.True(result);

        
    }

    [Test]
    public void TestAddRecToMissingFile()
    {
        // delete file and make sure we can add

        FileManager fm = new FileManager();
        fm.DeleteFile();
        bool result = fm.AddRecord("abc,Jim,B");
        Assert.True(result);


        
    }


    [Test]
    public void TestAddRecordToFileNotDuplicate()
    {
        
        FileManager fm = new FileManager();
        fm.DeleteFile();

        
        bool result = fm.AddRecord("abc,Jim,B");

        
        Assert.True(result); // Check that the record was added successfully

        // Check if the record exists in the file
        string[] lines = File.ReadAllLines(FileManager.filePath);
        bool recordFound = false;
        foreach (string line in lines)
        {
            if (line == "abc,Jim,B")
            {
                recordFound = true;
                break;
            }
        }
        Assert.True(recordFound, "Record was not found in the file.");
    }


    [Test]
    public void TestAddRecordToFileDuplicate()
    {
        
        FileManager fm = new FileManager();
        fm.DeleteFile();

        // Add a recrod
        bool result = fm.AddRecord("abc,Jim,B");
        Assert.True(result, "Could not add record in TestAddRecordToFileDuplicate");


        // Add same record which should be a duplicate
        result = fm.AddRecord("abc,Jim,B");

        // Assert
        Assert.False (result); // Check that the record was added successfully

     }


    [Test]
    public void TestAddRecToFileNotEnoughFields()
    {
        FileManager fm = new FileManager();
        bool result = fm.AddRecord("abc");

        Assert.False(result);
    }


    [Test]
    public void TestAddRecToFileTooManyFields()
    {
        FileManager fm = new FileManager();
        bool result = fm.AddRecord("abc,c,d,e,f");
        Assert.False(result);
        
    }

    [Test]
    public void ThisShouldFail()
    {
        Assert.False(true);
    }



    [Test]
    public void TestAddLotsOfRecords()
    {
        FileManager fm = new FileManager();
        fm.DeleteFile();

        


        

        bool result;
        result = fm.AddRecord("a,a,b");
        Assert.True(result);

        for ( int i = 0; i<1000; ++i)
        {
            String ID = Guid.NewGuid().ToString();
            result = fm.AddRecord("a,a,b");
            Assert.False(result); // ensure no dups can add

            result = fm.AddRecord($"{ID},a,b");
            Assert.True(result); // Id should be unique, names duplicated

        }
    }

   
    [Test]
    public void TestAddDuplicateName()
    {
        FileManager fm = new FileManager();
        fm.DeleteFile();

        Boolean result;
        int lines;

        // These are all good records with no duplicate ids

        result = fm.AddRecord("a,b,c");
        Assert.True(result);
        lines = LineCount(FileManager.filePath);
        Assert.Greater(lines,0);

        result = fm.AddRecord("b,b,c");
        Assert.True(result);
        lines = LineCount(FileManager.filePath);
        Assert.Greater(lines, 1);

        result = fm.AddRecord("c,b,c");
        Assert.True(result);
        lines = LineCount(FileManager.filePath);
        Assert.Greater(lines, 2);

        result = fm.AddRecord("abc,Joe,Smith");
        Assert.True(result);
        lines = LineCount(FileManager.filePath);
        Assert.Greater(lines, 3);

        result = fm.AddRecord("cde,Joe,Smith");
        Assert.True(result);
        lines = LineCount(FileManager.filePath);
        Assert.Greater(lines, 4);

        result = fm.AddRecord("efg,Joe,Smith");
        Assert.True(result);
        lines = LineCount(FileManager.filePath);
        Assert.Greater(lines, 5);

    }

    public int LineCount(string filePath)
    {
        if (File.Exists(filePath))
        {
            int lineCount = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            return lineCount;
        }
        else
        {
            return 0; // Return 0 if the file doesn't exist
        }
    }




}
